using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Supported by objects that can be created or altered.
    /// </summary>
    public interface ISqlServerAlterOrCreateStatement : ISqlServerObject
    {
        /// <summary>
        /// Gets whether the <see cref="CreateOrAlterStatementPrefix"/> of this statement.
        /// </summary>
        CreateOrAlterStatementPrefix StatementPrefix { get; }
        
        /// <summary>
        /// Returns a new <see cref="ISqlServerAlterOrCreateStatement"/> with the specified prefix.
        /// </summary>
        /// <param name="prefix">The new prefix.</param>
        /// <returns>The same object with a changed statement prefix.</returns>
        ISqlServerAlterOrCreateStatement WithStatementPrefix( CreateOrAlterStatementPrefix prefix );

    }
}
