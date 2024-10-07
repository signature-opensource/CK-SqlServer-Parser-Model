using System;
using CK.Core;

namespace CK.SqlServer.Parser;

/// <summary>
/// Captures parsing error or sucessful <see cref="Result"/>.
/// </summary>
public interface ISqlServerParserResult<T> where T : class, ISqlServerParsedText
{
    /// <summary>
    /// Gets the result. Null on error.
    /// </summary>
    T Result { get; }

    /// <summary>
    /// Gets the error message.
    /// Null if <see cref="IsError"/> is false.
    /// </summary>
    string ErrorMessage { get; }

    /// <summary>
    /// Gets the source where the error occurs.
    /// Null if <see cref="IsError"/> is false.
    /// </summary>
    string HeadSource { get; }

    /// <summary>
    /// True on error.
    /// </summary>
    bool IsError { get; }

    /// <summary>
    /// Logs the error message if <see cref="IsError"/> is true, otherwise does nothing.
    /// </summary>
    /// <param name="monitor">Monitor to log into. Must not be null.</param>
    /// <param name="asWarning">True to log a warning instead of an error.</param>
    void LogOnError( IActivityMonitor monitor, bool asWarning = false );
}
