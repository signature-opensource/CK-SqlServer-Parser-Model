using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Defines a view.
    /// </summary>
    public interface ISqlServerView : ISqlServerAlterOrCreateStatement
    {
        /// <summary>
        /// Gets the column names explicitly declared by this view:
        /// <c>create view Schema.Name( Column1, Column2 ) as select ...</c>.
        /// Null if the columns are not specified.
        /// </summary>
        IReadOnlyList<string> FormalColumnList { get; }
    }
}
