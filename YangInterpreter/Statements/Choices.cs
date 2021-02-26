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
    public class Choices : StatementBase
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

        public XElement[] NodeAsXmlForUses()
        {
            List<XElement> retchildlist = new List<XElement>();
            foreach (var child in StatementList)
            {
                retchildlist.AddRange(child.StatementAsXML());
            }
            return retchildlist.ToArray();
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
