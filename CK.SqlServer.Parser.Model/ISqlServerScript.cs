using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser;

/// <summary>
/// A script contains one or more statements, 'GO' being considered as a statement like any other.
/// </summary>
public interface ISqlServerScript : ISqlServerParsedText
{
}
