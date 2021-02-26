using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Container : StatementBase
    {
        public Container(string Value) : base("Container", Value) { }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        internal override string StatementAsYangString()
        {
            string retval = string.Format("container {0} {{\r\n", Name);
            foreach (var child in StatementList)
            {
                retval += child.StatementAsYangString(1)+"\r\n";
            }
            retval += "}";
            return retval;
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "container " + Name + " {" + Environment.NewLine;
            strBuilder += GetStatementsAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
