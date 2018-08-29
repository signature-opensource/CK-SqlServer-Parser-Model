using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK.SqlServer.Parser
{
    /// <summary>
    /// Modelizes a sql server parameter.
    /// </summary>
    public interface ISqlServerParameter
    {
        /// <summary>
        /// Gets whether the parameter is a input only parameter.
        /// </summary>
        bool IsPureInput { get; }

        /// <summary>
        /// Gets whether the parameter is an input parameter or an output one with a /*input*/ comment.
        /// </summary>
        bool IsInput { get; }

        /// <summary>
        /// Gets whether the parameter is output. It can be /*input*/output (see <see cref="IsInputOutput"/>).
        /// </summary>
        bool IsOutput { get; }

        /// <summary>
        /// Gets whether the parameter is an output only parameter (ie. it is <see cref="IsOutput"/> but not <see cref="IsInputOutput"/>).
        /// </summary>
        bool IsPureOutput { get; }

        /// <summary>
        /// Gets whether the parameter is input and output (by ref). <see cref="IsOutput"/> is true: 
        /// the parameter uses the '/*input*/output' syntax.
        /// </summary>
        bool IsInputOutput { get; }

        /// <summary>
        /// Gets whether the parameter is read only.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets whether the parameter is marked with the '/*not null*/ comment.
        /// </summary>
        bool IsNotNull { get; }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        ISqlServerUnifiedTypeDecl SqlType { get; }

        /// <summary>
        /// Gets the default value or null if no default are defined.
        /// </summary>
        ISqlServerParameterDefaultValue DefaultValue { get; }

        /// <summary>
        /// Gets the parameter representation without comments nor extraneous white spaces on a sigle line.
        /// </summary>
        /// <returns>A clean parameter representation.</returns>
        string ToStringClean();
    }
}
