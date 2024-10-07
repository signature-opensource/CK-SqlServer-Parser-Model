using CK.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser;

/// <summary>
/// A transformer applies to a target object or to any script.
/// </summary>
public interface ISqlServerTransformer : ISqlServerParsedText
{
    /// <summary>
    /// Gets the optional target schema name.
    /// Just like <see cref="ISqlServerObject.SchemaName"/>, this is not escaped.
    /// Can be null for an unscoped transformer.
    /// </summary>
    string TargetSchemaName { get; }

    /// <summary>
    /// Gets the optional name of this transformer (null for an anonymous transformer).
    /// Transformer's name use the same syntax as actual <see cref="ISqlServerObject"/> for consistency.
    /// Just like <see cref="ISqlServerObject.SchemaName"/>, this is not escaped, but please,
    /// do not use quoted identifiers for transformers!
    /// </summary>
    string SchemaName { get; }

    /// <summary>
    /// Applies this transformation to an object.
    /// This uses untyped object: there is no guaranty that a transformation
    /// does not change the type of the transformed object.
    /// Use <see cref="SqlServerExtension.SafeTransform{T}(ISqlServerTransformer, IActivityMonitor, T)">SafeTransform</see> 
    /// extension method to ensure type preservation.
    /// </summary>
    /// <param name="monitor">The monitor that will receive logs and errors. Can not be null.</param>
    /// <param name="target">The target object to transform. Can not be null.</param>
    /// <returns>The transformed object on success and null if an error occurred.</returns>
    object Transform( IActivityMonitor monitor, object target );
}
