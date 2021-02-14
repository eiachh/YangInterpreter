using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public class EmptyLineStatement : BaseStatement
    {
        public EmptyLineStatement() : base("") { }
        public EmptyLineStatement(string empty) : this() { }
        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement(" ") };
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            return "";
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
