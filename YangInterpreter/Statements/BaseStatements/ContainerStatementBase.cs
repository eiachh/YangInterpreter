using System;
using System.Xml.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class ContainerStatementBase : BaseStatement
    {
        public ContainerStatementBase(string Name) : base(Name) { }
        public ContainerStatementBase(string Name, string Value) : base(Name) { base.Value = Value; }
        public override XElement[] StatementAsXML()
        {
            XElement ThisStatementAsXml = new XElement(base.Name, base.Value);
            foreach (var desc in Descendants())
            {
                ThisStatementAsXml.Add(desc.StatementAsXML());
            }
            return new XElement[] { ThisStatementAsXml };
        }

        public override string StatementAsYangString(int IndentationLevel)
        {
            var indent = GetIndentation(IndentationLevel);
            var stringBuilder = indent + Name + " " + Value + " {" + Environment.NewLine;
            stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
            stringBuilder += indent + "}";
            return stringBuilder;
        }
    }
}
