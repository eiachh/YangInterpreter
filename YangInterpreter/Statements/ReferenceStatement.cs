using System;
using System.Collections.Generic;
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
    public class ReferenceStatement : ChildlessStatement
    {
        public ReferenceStatement() : base("reference") { }
        public ReferenceStatement(string Value) : this() { base.Argument = Value; }

        internal override bool IsQuotedValue => true;
    }
}
