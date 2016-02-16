using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// General object definition with an <see cref="ObjectName"/> and an <see cref="ObjectType"/>.
    /// </summary>
    public interface ISqlServerObject
    {
        /// <summary>
        /// Gets the name of this object, including its schema if any.
        /// </summary>
        string ObjectName { get; }

        /// <summary>
        /// Gets the schema of this object if there is one (null otherwise).
        /// </summary>
        string SchemaName { get; }

        /// <summary>
        /// Returns a new <see cref="ISqlServerObject"/> with the given schema name.
        /// When null, the schema is removed.
        /// </summary>
        /// <returns>The same object with a changed schema name.</returns>
        ISqlServerObject SetSchemaName( string name );

        /// <summary>
        /// Gets the type of this object.
        /// </summary>
        SqlServerObjectType ObjectType { get; }

        /// <summary>
        /// Writes the header of this object, optionally with its options.
        /// </summary>
        /// <param name="withOptions">True to obtain object options if any.</param>
        /// <returns>The object header.</returns>
        string ToStringSignature( bool withOptions );

        /// <summary>
        /// Writes full object text to a StringBuilder.
        /// </summary>
        /// <param name="b">The StringBuilder to write to.</param>
        void Write( StringBuilder b );

    }
}
