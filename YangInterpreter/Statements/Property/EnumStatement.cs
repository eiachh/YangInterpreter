using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Property
{
    public class EnumStatement : Statement
    {
        public EnumStatement() : base("enum") { }
        public EnumStatement(string value) : base("enum") 
        {
            Value = value;
        }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            throw new NotImplementedException();
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
