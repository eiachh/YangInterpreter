using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Position : ChildlessContainerStatement
    {
        internal override bool IsQuotedValue => true;
        public Position(string Value) : base("Position",Value) { }

        internal override bool IsValueStartAtSameLine() { return true; }
    }
}
