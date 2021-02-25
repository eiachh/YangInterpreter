using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Base Statement RFC 6020 7.16.2. 
    /// 
    /// <summary>
    /// The "base" statement, which is optional, takes as an argument a
    /// string that is the name of an existing identity, from which the new
    /// identity is derived.If no "base" statement is present, the identity
    /// is defined from scratch.
    /// </summary>
    public class BaseStatement : ChildlessContainerStatement
    {
        public BaseStatement() : base("Base") { }
        public BaseStatement(string Argument) : base("Base",Argument) { }

        internal override bool IsQuotedValue => true;
    }
}
