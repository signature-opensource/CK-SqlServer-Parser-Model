#region Proprietary License
/*----------------------------------------------------------------------------
* This file (CK.SqlServer.Parser\Parser\Expr\ISqlExprUnifiedTypeDecl.cs) is part of CK-Database. 
* Copyright © 2007-2014, Invenietis <http://www.invenietis.com>. All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System.Data;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Unifies Sql types.
    /// </summary>
    /// <remarks>
    /// This is not an attempt to model the actual type capacity, but only focuses on syntax representation. <see cref="SqlDbType.DateTime"/> for example
    /// has a Precision of 23 and a Scale of 3 in terms of digits, but here, we consider Precision and Scale to be 0 (non applicable) since 'datetime(p,s)' is not valid.
    /// To make this more explicit, the Size/Precision/Scale properties have been prefixed with 'Syntax'.
    /// </remarks>
    public interface ISqlServerUnifiedTypeDecl
    {
        /// <summary>
        /// Gets the database type.
        /// </summary>
        SqlDbType DbType { get; }

        /// <summary>
        /// Gets a positive size if it is specified, 0 when not specified (see below), -1 for 'max' (like in nvarchar(max)) 
        /// and -2 when not applicable (for example when <see cref="DbType"/> is <see cref="SqlDbType.Int"/>).
        /// When not specified (0), the actual value can be 1 or 30: in a data definition or variable declaration statement, the default length is 1. 
        /// When using the CAST and CONVERT functions, the default length is 30.
        /// </summary>
        int SyntaxSize { get; }

        /// <summary>
        /// The maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point.
        /// The precision must be a value from 1 through the maximum precision of 38 (0 when not applicable or not specified). 
        /// The default precision is 18.
        /// </summary>
        byte SyntaxPrecision { get; }

        /// <summary>
        /// Gets the number scale: it is the number of digits to the right of the decimal point in a number. 
        /// 0 is the default and is used also when not applicable. This must always be true: 0 &lt;= scale &lt;= precision.
        /// </summary>
        byte SyntaxScale { get; }
        
        /// <summary>
        /// Gets the fractional seconds precision (actually is the scale) of <see cref="SqlDbType.DateTime2"/>, <see cref="SqlDbType.Time"/> and <see cref="SqlDbType.DateTimeOffset"/>.
        /// Can be between 0 and 7. -1 when not applicable (for any other <see cref="DbType"/>).
        /// </summary>
        int SyntaxSecondScale { get; }

        /// <summary>
        /// Gets a clean text representation on one line of this type.
        /// </summary>
        /// <returns>A clean representation.</returns>
        string ToStringClean();
    }
}
