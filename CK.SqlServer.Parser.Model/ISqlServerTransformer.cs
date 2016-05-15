using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// A transformer applies to a target object or to any script.
    /// </summary>
    public interface ISqlServerTransformer : ISqlServerParsedText
    {
        /// <summary>
        /// Gets the optional target schema name.
        /// Can be null.
        /// </summary>
        string TargetSchemaName { get; }

    }
}
