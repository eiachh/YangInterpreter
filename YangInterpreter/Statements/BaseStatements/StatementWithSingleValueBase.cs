using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class StatementWithSingleValueBase : BaseStatement
    {
        public StatementWithSingleValueBase(string Name) : base(Name) { }
        public StatementWithSingleValueBase(string Name,string Value) : base(Name) { base.Value = Value; }
        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement("Description", base.Value) };
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if (GeneratedFrom == TokenTypes.DescriptionSameLineStart)
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.SameLineStart);
            else
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.NextLineStart);
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
