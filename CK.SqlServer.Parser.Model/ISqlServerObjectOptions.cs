using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Models options for <see cref="ISqlServerObject"/> objects.
    /// </summary>
    public interface ISqlServerObjectOptions
    {
        /// <summary>
        /// Gets whether this object is schema bound.
        /// </summary>
        bool SchemaBinding { get; }
    }
}
