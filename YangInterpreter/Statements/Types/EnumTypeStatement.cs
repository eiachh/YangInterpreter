using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    public class EnumTypeStatement : TypeStatement
    {
        public EnumTypeStatement() : base(BuiltInTypes.enumeration) { }
        public EnumTypeStatement(string value) : base(BuiltInTypes.enumeration) { }
    }
}
