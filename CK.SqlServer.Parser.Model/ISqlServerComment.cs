using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Defines a comment.
    /// </summary>
    public interface ISqlServerComment
    {
        /// <summary>
        /// Gets whether this comment is a // line comment.
        /// Otherwise, it is a /* star comment */.
        /// </summary>
        bool IsLineComment { get; }

        /// <summary>
        /// Gets the text without // nor end of line when <see cref="IsLineComment"/> is true
        /// and without /* and */ otherwise.
        /// </summary>
        string Text { get; }
    }

}
