using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser;

/// <summary>
/// Defines the different types of Sql Server objects.
/// </summary>
public enum SqlServerObjectType
{
    /// <summary>
    /// Not a known type.
    /// </summary>
    None,

    /// <summary>
    /// Stored procedure.
    /// </summary>
    Procedure,

    /// <summary>
    /// View.
    /// </summary>
    View,

    /// <summary>
    /// Scalar function.
    /// </summary>
    ScalarFunction,

    /// <summary>
    /// Inline table function.
    /// </summary>
    InlineTableFunction,

    /// <summary>
    /// Multi-statement table function.
    /// </summary>
    MultiStatementTableFunction
}
