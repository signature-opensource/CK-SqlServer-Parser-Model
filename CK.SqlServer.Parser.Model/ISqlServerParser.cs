using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Primary parser contract.
    /// </summary>
    public interface ISqlServerParser
    {
        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerObject"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="sqlObject">The parsed object. Null on error.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserError ParseObject( string text, out ISqlServerObject sqlObject );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerStoredProcedure"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="sqlProcedure">The parsed procedure. Null on error.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserError ParseStoredProcedure( string text, out ISqlServerStoredProcedure sqlProcedure );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerFunctionScalar"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="sqlFScalar">The parsed scalar function. Null on error.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserError ParseStoredFunctionScalar(string text, out ISqlServerFunctionScalar sqlFScalar);

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerFunctionTable"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="sqlFTable">The parsed scalar multi-statement function. Null on error.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserError ParseStoredFunctionTable( string text, out ISqlServerFunctionTable sqlFTable );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerFunctionInlineTable"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="sqlFInlineTable">The parsed scalar inline table function. Null on error.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserError ParseStoredFunctionInlineTable( string text, out ISqlServerFunctionInlineTable sqlFInlineTable );
    }
}
