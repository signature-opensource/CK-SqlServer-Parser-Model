using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    public interface ISqlServerParser
    {
        ISqlServerParserError ParseObject( string text, out ISqlServerObject sqlObject );

        ISqlServerParserError ParseStoredProcedure( string text, out ISqlServerStoredProcedure sqlProcedure );
    }
}
