using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Path Statement RFC 6020 9.9.2.
    ///
    /// <summary>
    /// The "path" statement, which is a substatement to the "type"
    /// statement, MUST be present if the type is "leafref". It takes as an
    /// argument a string that MUST refer to a leaf or leaf-list node.
    /// </summary>
    public class PathStatement : ChildlessContainerStatement
    {
        internal override bool IsQuotedValue => true;
        public PathStatement() : base("Path") { }
        public PathStatement(string Value) : base("Path", Value) { }
    }
}
