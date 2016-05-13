using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Basic interface that generalizes any kind of parsed object.
    /// </summary>
    public interface ISqlServerParsedText
    {
        /// <summary>
        /// Gets the comments that are before the first token.
        /// </summary>
        IEnumerable<ISqlServerComment> HeaderComments { get; }

        /// <summary>
        /// Writes full text to a StringBuilder.
        /// </summary>
        /// <param name="b">The StringBuilder to write to.</param>
        void Write( StringBuilder b );

    }
}
