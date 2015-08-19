using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Generalizes objects that can be called: they all have parameters.
    /// </summary>
    public interface ISqlServerCallableObject : ISqlServerObject
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        ISqlServerParameterList Parameters { get; }
    }
}
