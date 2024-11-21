using CK.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser;


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
    static public string ToFullString( this ISqlServerParsedText @this )
    {
        StringBuilder b = new StringBuilder();
        @this.Write( b );
        return b.ToString();
    }

    /// <summary>
    /// Applies this transformation to an object, ensuring that the object's type is preserved.
    /// </summary>
    /// <typeparam name="T">Type of the object to transform.</typeparam>
    /// <param name="this">This transformer.</param>
    /// <param name="monitor">The monitor that will receive logs and errors. Can not be null.</param>
    /// <param name="target">The target object to transform. Can not be null.</param>
    /// <returns>The transformed object on success and null if an error occurred.</returns>
    static public T SafeTransform<T>( this ISqlServerTransformer @this, IActivityMonitor monitor, T target ) where T : class, ISqlServerParsedText
    {
        object o = @this.Transform( monitor, target );
        if( o == null ) return null;
        T r = o as T;
        if( r == null ) monitor.Error( $"Transformation applied to {typeof( T ).Name} produced a {o.GetType().Name} object. Object's type should not be altered." );
        return r;
    }

}
