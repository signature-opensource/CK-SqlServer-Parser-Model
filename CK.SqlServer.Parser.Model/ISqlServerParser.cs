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
        /// Parses the text and tries to extract any <see cref="ISqlServerParsedText"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerParsedText> Parse( string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerScript"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerScript> ParseScript( string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerTransformer"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerTransformer> ParseTransformer( string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerObject"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerObject> ParseObject( string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerStoredProcedure"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerStoredProcedure> ParseStoredProcedure( string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerFunctionScalar"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerFunctionScalar> ParseFunctionScalar(string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerFunctionTable"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerFunctionTable> ParseFunctionTable( string text );

        /// <summary>
        /// Parses the text and tries to extract a <see cref="ISqlServerFunctionInlineTable"/> from it.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A result object.</returns>
        ISqlServerParserResult<ISqlServerFunctionInlineTable> ParseFunctionInlineTable( string text );
    }
}
