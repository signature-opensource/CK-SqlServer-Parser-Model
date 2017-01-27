using Cake.Common;
using Cake.Common.Solution;
using Cake.Common.IO;
using Cake.Common.Tools.NUnit;
using Cake.Common.Tools.MSBuild;
using Cake.Common.Tools.NuGet;
using Cake.Core;
using SimpleGitVersion;
using Cake.Common.Diagnostics;
using Code.Cake;
using Cake.Common.Text;
using Cake.Common.Tools.NuGet.Pack;
using System;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Common.Tools.NuGet.Push;
using Cake.Core.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;

namespace CodeCake
{
    /// <summary>
    /// Sample build "script".
    /// It can be decorated with AddPath attributes that inject paths into the PATH environment variable. 
    /// </summary>
    [AddPath("CodeCakeBuilder/tools")]
    [AddPath("packages/**/tools*")]
    public class Build : CodeCakeHost
    {
        public Build()
        {
            const string solutionName = "CK-SqlServer-Parser-Model";
            const string solutionFileName = solutionName + ".sln";

            var releasesDir = Cake.Directory("CodeCakeBuilder/Releases");

            // We do not publish .Tests projects for this solution.
            var projectsToPublish = Cake.ParseSolution(solutionFileName)
                                        .Projects
                                        .Where(p => !(p is SolutionFolder)
                                                    && p.Name != "CodeCakeBuilder"
                                                    && !p.Path.Segments.Contains("Tests"));

            var jsonS = Cake.GetSimpleJsonSolution();
            SimpleRepositoryInfo gitInfo = jsonS.RepositoryInfo;
            const string configuration = "Release";

            Teardown(cake =>
            {
                if (gitInfo.IsValid)
                {
                    jsonS.RestoreProjectFiles();
                }
            });

            Task("Check-Repository")
                .Does(() =>
                {
                    if (!gitInfo.IsValid)
                    {
                        if (Cake.IsInteractiveMode()
                            && Cake.ReadInteractiveOption("Repository is not ready to be published. Proceed anyway?", 'Y', 'N') == 'Y')
                        {
                            Cake.Warning("GitInfo is not valid, but you choose to continue...");
                        }
                        else throw new Exception("Repository is not ready to be published.");
                    }
                    else jsonS.UpdateProjectFiles(useNuGetV2Version: true);

                    Cake.Information("Publishing {0} projects with version={1} and configuration={2}: {3}",
                        projectsToPublish.Count(),
                        gitInfo.SemVer,
                        configuration,
                        string.Join(", ", projectsToPublish.Select(p => p.Name)));
                });

            Task("Restore-NuGet-Packages")
                .Does(() =>
                {
                    Cake.DotNetCoreRestore();
                });

            Task("Clean")
                .IsDependentOn("Check-Repository")
                .Does(() =>
                {
                    Cake.CleanDirectories("**/bin/" + configuration, d => !d.Path.Segments.Contains("CodeCakeBuilder"));
                    Cake.CleanDirectories("**/obj/" + configuration, d => !d.Path.Segments.Contains("CodeCakeBuilder"));
                    Cake.CleanDirectories(releasesDir);
                });

            Task("Build")
                .IsDependentOn("Clean")
                .IsDependentOn("Restore-NuGet-Packages")
                .IsDependentOn("Check-Repository")
                .Does(() =>
                {
                    using (var tempSln = Cake.CreateTemporarySolutionFile(solutionFileName))
                    {
                        tempSln.ExcludeProjectsFromBuild("CodeCakeBuilder");
                        Cake.MSBuild(tempSln.FullPath, settings =>
                        {
                            settings.Configuration = configuration;
                            settings.Verbosity = Verbosity.Normal;
                        });
                    }
                });

            Task("Create-NuGet-Packages")
                .IsDependentOn("Build")
                .Does(() =>
               {
                   Cake.CreateDirectory(releasesDir);
                   foreach (SolutionProject p in projectsToPublish)
                   {
                       Cake.Warning(p.Path.GetDirectory().FullPath);
                       Cake.DotNetCorePack(p.Path.GetDirectory().FullPath, new DotNetCorePackSettings()
                       {
                           Verbose = true,
                           NoBuild = true,
                           Configuration = configuration,
                           OutputDirectory = releasesDir
                       });
                   }
               });

            Task("Push-NuGet-Packages")
                .IsDependentOn("Create-NuGet-Packages")
                .WithCriteria(() => gitInfo.IsValid)
                .Does(() =>
               {
                   IEnumerable<FilePath> nugetPackages = Cake.GetFiles(releasesDir.Path + "/*.nupkg");
                   if (Cake.IsInteractiveMode())
                   {
                       var localFeed = Cake.FindDirectoryAbove("LocalFeed");
                       if (localFeed != null)
                       {
                           Cake.Information("LocalFeed directory found: {0}", localFeed);
                           if (Cake.ReadInteractiveOption("Do you want to publish to LocalFeed?", 'Y', 'N') == 'Y')
                           {
                               Cake.CopyFiles(nugetPackages, localFeed);
                           }
                       }
                   }
                   if (gitInfo.IsValidRelease)
                   {
                       if (gitInfo.PreReleaseName == ""
                           || gitInfo.PreReleaseName == "rc"
                           || gitInfo.PreReleaseName == "prerelease")
                       {
                           PushNuGetPackages("NUGET_API_KEY", "https://www.nuget.org/api/v2/package", nugetPackages);
                       }
                       else
                       {
                            // An alpha, beta, delta, epsilon, gamma, kappa, prerelease goes to invenietis-preview.
                            PushNuGetPackages("MYGET_PREVIEW_API_KEY", "https://www.myget.org/F/invenietis-preview/api/v2/package", nugetPackages);
                       }
                   }
                   else
                   {
                       Debug.Assert(gitInfo.IsValidCIBuild);
                       PushNuGetPackages("MYGET_CI_API_KEY", "https://www.myget.org/F/invenietis-ci/api/v2/package", nugetPackages);
                   }
               });

            Task("Default").IsDependentOn("Push-NuGet-Packages");
        }

        void PushNuGetPackages(string apiKeyName, string pushUrl, IEnumerable<FilePath> nugetPackages)
        {
            // Resolves the API key.
            var apiKey = Cake.InteractiveEnvironmentVariable(apiKeyName);
            if (string.IsNullOrEmpty(apiKey))
            {
                Cake.Information("Could not resolve {0}. Push to {1} is skipped.", apiKeyName, pushUrl);
            }
            else
            {
                var settings = new NuGetPushSettings
                {
                    Source = pushUrl,
                    ApiKey = apiKey
                };

                foreach (var nupkg in nugetPackages.Where(p => !p.FullPath.EndsWith(".symbols.nupkg")))
                {
                    Cake.NuGetPush(nupkg, settings);
                }
            }
        }
    }
}
