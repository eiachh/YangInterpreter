using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.SingleValueStatements
{
    class Position : StatementWithSingleValueBase
    {
        public Position(string Name) : base(Name) { }

        public Position(string Name, string Value) : base(Name, Value) { }

        internal override bool IsValueStartAtSameLine() { return true; }
    }
}
