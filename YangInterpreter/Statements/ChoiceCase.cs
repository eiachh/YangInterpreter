using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// <summary>
    /// Case statement insode choice.
    /// </summary>
    class ChoiceCase : StatementBase
    {
        public ChoiceCase(string Value) : base("Case",Value) { }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "case " + Name + " {" + Environment.NewLine;
            strBuilder += GetStatementsAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
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
