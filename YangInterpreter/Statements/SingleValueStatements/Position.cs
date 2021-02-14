using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Position : StatementWithSingleValueBase
    {
        public Position(string Value) : base("Position",Value) { }

        internal override bool IsValueStartAtSameLine() { return true; }
    }
}
