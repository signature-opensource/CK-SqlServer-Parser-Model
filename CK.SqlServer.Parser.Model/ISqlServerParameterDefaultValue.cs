using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    public interface ISqlServerParameterDefaultValue
    {
        bool IsVariable { get; }

        bool IsNull { get; }
        
        bool IsLiteral { get; }

        bool HasMinusSign { get; }

        /// <summary>
        /// Gets the default value (<see cref="IsVariable"/> must be false).
        /// It can be <see cref="DBNull.Value"/>, a <see cref="Int32"/>, <see cref="Decimal"/>, a <see cref="Double"/> or a string for 
        /// too big numerics (that exceed Decimal .Net capacity) and money:
        /// .Net <see cref="Decimal"/> type has only 28 digits whereas Sql server numerics has 38. And money is actually a Int64 for
        /// sql server.
        /// </summary>
        object NullOrLitteralDotNetValue { get; }
    }
}
