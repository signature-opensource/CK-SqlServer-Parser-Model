using System;
using System.Collections.Generic;
using System.Text;

namespace CK.SqlServer.Parser;

/// <summary>
/// Defines the "create"/"alter"/"create or alter" statement prefix. 
/// </summary>
public enum CreateOrAlterStatementPrefix
{
    /// <summary>
    /// Not applicable.
    /// </summary>
    None,

    /// <summary>
    /// Create only prefix.
    /// </summary>
    Create,

    /// <summary>
    /// Alter only prefix.
    /// </summary>
    Alter,

    /// <summary>
    /// Create or alter prefix (since SQL Server 2016 SP1).
    /// </summary>
    CreateOrAlter
}
