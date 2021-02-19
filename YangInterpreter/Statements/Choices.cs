using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using System.Xml.Linq;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// <summary>
    /// Choice statement itself.
    /// </summary>
    public class Choices : ContainerStatementBase
    { 
        public Choices(string Value) : base("Choices",Value){ }

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

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }

        /*internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }*/
    }
}
