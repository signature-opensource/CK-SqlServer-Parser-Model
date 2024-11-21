using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser;

/// <summary>
/// Defines default value for a <see cref="ISqlServerParameter"/>: a default value can be a 
/// variable (another parameter), null, a literal and may have a minus sign.
/// </summary>
public interface ISqlServerParameterDefaultValue
{
    /// <summary>
    /// Gets whether this default value is a parameter name.
    /// </summary>
    bool IsVariable { get; }

    /// <summary>
    /// Gets whether the default value is null.
    /// </summary>
    bool IsNull { get; }

    /// <summary>
    /// Gets whether this default value is a literal.
    /// </summary>
    bool IsLiteral { get; }

    /// <summary>
    /// Gets whether a minus sign appears in front of the default value.
    /// </summary>
    bool HasMinusSign { get; }

    /// <summary>
    /// Gets the default value (<see cref="IsVariable"/> must be false).
    /// It can be <see cref="DBNull.Value"/>, a <see cref="Int32"/>, <see cref="Decimal"/>, a <see cref="Double"/> or 
    /// a string for too big numerics (that exceed Decimal .Net capacity) and money:
    /// .Net <see cref="Decimal"/> type has only 28 digits whereas Sql server numerics has 38. And money is actually 
    /// a Int64 for sql server.
    /// </summary>
    object NullOrLitteralDotNetValue { get; }
}
