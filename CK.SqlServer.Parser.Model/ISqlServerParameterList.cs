using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser;

/// <summary>
/// A read-only list of <see cref="ISqlServerParameter"/>.
/// </summary>
public interface ISqlServerParameterList : IReadOnlyList<ISqlServerParameter>
{
    /// <summary>
    /// Gets a clean text representation on one line of the parameters.
    /// </summary>
    /// <returns>A clean representation.</returns>
    string ToStringClean();
}
