using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Defines a function that returns a scalar.
    /// </summary>
    public interface ISqlServerFunctionScalar : ISqlServerCallableObject
    {
        /// <summary>
        /// Gets the returned type.
        /// </summary>
        ISqlServerUnifiedTypeDecl ReturnType { get; }
    }
}
