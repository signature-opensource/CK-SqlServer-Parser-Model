using System;
using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.MSBuild;
using Cake.Common.Tools.SignTool;
using Cake.Core;
using Cake.Core.Diagnostics;
using Code.Cake;
using SimpleGitVersion;
using Cake.Common.Tools.NuGet.Pack;
using System.Collections.Generic;
using Cake.Common.Tools.NuGet.Push;

namespace CodeCake
{
    public class Build : CodeCakeHost
    {
        public Build()
        {
            var configuration = Cake.Argument( "configuration", "Release" );
            var securePath = Cake.Argument( "securePath", "../../_Secure" );
            var secureDir = Cake.Directory( securePath );

            var nugetOutputDir = Cake.Directory( "CodeCake/Release" );
            SimpleRepositoryInfo gitInfo = null;
            SignToolSignSettings signSettingsForRelease = null;

            // Define directories.
            var buildDir = Cake.Directory( "CK.SqlServer.Parser.Model/bin" ) + Cake.Directory( configuration );

            Task( "Clean" )
                .Does( () =>
                {
                    Cake.CleanDirectory( buildDir );
                } );

            Task( "Restore-NuGet-Packages" )
                .IsDependentOn( "Clean" )
                .Does( () =>
                {
                    Cake.NuGetRestore( "CK-SqlServer-Parser-Model.sln" );
                } );

            Task( "Build" )
                .IsDependentOn( "Restore-NuGet-Packages" )
                .Does( () =>
                {
                    Cake.MSBuild( "CK.SqlServer.Parser.Model/CK.SqlServer.Parser.Model.csproj", new MSBuildSettings()
                        .UseToolVersion( MSBuildToolVersion.NET45 )
                        .SetVerbosity( Verbosity.Normal )
                        .SetConfiguration( configuration ) );
                } );

            Task( "Check-Publish" )
                .Does( () =>
                {
                    gitInfo = Cake.GetSimpleRepositoryInfo();
                    if( !gitInfo.IsValid ) throw new Exception( "SimpleGitVersionInfo: This solution is not ready for publishing." );
                    else if( !Cake.DirectoryExists( secureDir ) ) throw new Exception( String.Format( "SecurePath '{0}' not found.", secureDir ) );
                    else
                    {
                        // If the release is a not a CI build, we must sign the artifacts before packaging.
                        if( gitInfo.IsValidRelease )
                        {
                            signSettingsForRelease = new SignToolSignSettings()
                            {
                                TimeStampUri = new Uri( "http://timestamp.verisign.com/scripts/timstamp.dll" ),
                                CertPath = secureDir + Cake.File( "Invenietis-Authenticode.pfx" ),
                                Password = System.IO.File.ReadAllText( secureDir + Cake.File( "Invenietis-Authenticode.p.txt" ) )
                            };
                        }
                        Cake.Log.Information( "Packages in version '{0}' can be published.", gitInfo.NuGetVersion );
                    }
                } );

            Task( "Sign-Authenticode" )
                .IsDependentOn( "Build" )
                .IsDependentOn( "Check-Publish" )
                .WithCriteria( () => signSettingsForRelease != null )
                .Does( () =>
                {
                    Cake.Sign( "CK.SqlServer.Parser.Model/bin/Release/CK.SqlServer.Parser.Model.dll", signSettingsForRelease );
                } );

            Task( "Create-NuGet-Package" )
                .IsDependentOn( "Build" )
                .IsDependentOn( "Check-Publish" )
                .IsDependentOn( "Sign-Authenticode" )
                .Does( () =>
                {
                    if( signSettingsForRelease != null )
                    {
                        Cake.Sign( "CK.SqlServer.Parser.Model/bin/Release/CK.SqlServer.Parser.Model.dll", signSettingsForRelease );
                    }
                    Cake.CreateDirectory( nugetOutputDir );
                    Cake.NuGetPack( "CodeCake/CK.SqlServer.Parser.Model.nuspec", new NuGetPackSettings()
                    {
                        Version = gitInfo.NuGetVersion,
                        BasePath = Cake.Environment.WorkingDirectory,
                        OutputDirectory = nugetOutputDir
                    } );
                } );

            Task( "Publish-NuGet-Package" )
                .IsDependentOn( "Create-NuGet-Package" )
                .Does( () =>
                {
                    foreach( var f in Cake.GetFiles( nugetOutputDir.Path.FullPath + "/*.nupkg" ) )
                    {
                        var settings = new NuGetPushSettings()
                        {
                            ApiKey = System.IO.File.ReadAllText( secureDir + Cake.File( "NuGet-Push-ApiKey.txt" ) ),
                            Verbosity = NuGetVerbosity.Detailed,
                            Source = "http://proget.app.invenietis.net/nuget/Default"
                        };
                        Cake.NuGetPush( f, settings );
                    }
                } );

            Task( "Default" ).IsDependentOn( "Publish-NuGet-Package" );
        }
    }
}
