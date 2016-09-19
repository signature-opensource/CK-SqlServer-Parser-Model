using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// General object definition with a <see cref="SchemaName"/> and an <see cref="ObjectType"/>.
    /// </summary>
    public interface ISqlServerObject : ISqlServerParsedText
    {
        /// <summary>
        /// Gets the name of this object, including its schema if any with no escaping:
        /// for <c>[a [schema]]]  .   [name]</c> this will be "[a [schema]]].[name]".
        /// </summary>
        string SchemaName { get; }

        /// <summary>
        /// Gets the schema of this object if there is one (null otherwise).
        /// This is the escaped schema: for <c>[a [schema]]]</c> this will be "a [schema]".
        /// </summary>
        string Schema { get; }

        /// <summary>
        /// Gets the ame of this object if there is one (null otherwise).
        /// This is the escaped name: for <c>[a [name]]]</c> this will be "a [name]".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a new <see cref="ISqlServerObject"/> with the given schema name.
        /// When null, the schema is removed.
        /// </summary>
        /// <returns>The same object with a changed schema name.</returns>
        ISqlServerObject SetSchema( string name );

        /// <summary>
        /// Gets the type of this object.
        /// </summary>
        SqlServerObjectType ObjectType { get; }

        /// <summary>
        /// Gets the options of this object.
        /// </summary>
        ISqlServerObjectOptions Options { get; }

        /// <summary>
        /// Writes the header of this object, optionally with its options.
        /// </summary>
        /// <param name="withOptions">True to obtain object options if any.</param>
        /// <returns>The object header.</returns>
        string ToStringSignature( bool withOptions );


    }
}
