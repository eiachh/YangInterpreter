using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Reference Statement RFC 6020 7.19.4. 
    /// 
    /// <summary>
    /// The "reference" statement takes as an argument a string that is used
    /// to specify a textual cross-reference to an external document, either
    /// another module that defines related management information, or a
    /// document that provides additional information relevant to this
    /// definition.
    /// </summary>
    public class Reference : StatementWithSingleValueBase
    {
        public Reference() : base("Reference") { }
        public Reference(string Value) : this() { base.Value = Value; }
        internal override bool IsValueStartAtSameLine()
        {
            return GeneratedFrom == TokenTypes.ReferenceSameLineStart;
        }
    }
}
