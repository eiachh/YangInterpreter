using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    public class BitsTypeStatement : TypeStatement
    {
        public BitsTypeStatement() : base(BuiltInTypes.bits) { }
        public BitsTypeStatement(string Name) : this() { }
    }
}
