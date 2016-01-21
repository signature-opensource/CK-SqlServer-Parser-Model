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
    public interface ISqlServerAlterOrCreateStatement
    {
        /// <summary>
        /// Gets whether this object is defined with a alter keyword.
        /// When false, it is a create statement.
        /// </summary>
        bool IsAlterKeyword { get; }
        
        /// <summary>
        /// Returns a new <see cref="ISqlServerAlterOrCreateStatement"/> with "create" if <see cref="IsAlterKeyword"/>
        /// is true, or an alter statement.
        /// </summary>
        /// <returns>The same object with a changed create/alter keyword.</returns>
        ISqlServerAlterOrCreateStatement ToggleKeyword();

    }
}
