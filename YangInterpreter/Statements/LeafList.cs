using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class LeafList : StatementBase
    {
        public LeafList(string Value) : base("Leaf-list",Value) { }

        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement(Name, "Example Content1"), new XElement(Name, "Example Content2") };
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
