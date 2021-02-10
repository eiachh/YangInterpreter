using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using System.Xml.Linq;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    public class Choices : Statement
    { 
        public Choices(string name) : base(name){ }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "choice " + Name + " {" + Environment.NewLine;
            strBuilder += GetStatementsAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }

        public override string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        public XElement[] NodeAsXmlForUses()
        {
            List<XElement> retchildlist = new List<XElement>();
            foreach (var child in StatementList)
            {
                retchildlist.AddRange(child.StatementAsXML());
            }
            return retchildlist.ToArray();
        }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }
    }
}
