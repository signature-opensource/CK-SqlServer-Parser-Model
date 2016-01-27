using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{

    /// <summary>
    /// Defines extension methods on models.
    /// </summary>
    public static class SqlServerExtension
    {
        /// <summary>
        /// Returns the full text of this object.
        /// </summary>
        /// <param name="this">This object.</param>
        /// <returns>The full text.</returns>
        static public string ToFullString( this ISqlServerObject @this )
        {
            StringBuilder b = new StringBuilder();
            @this.Write( b );
            return b.ToString();
        }
    }
}
