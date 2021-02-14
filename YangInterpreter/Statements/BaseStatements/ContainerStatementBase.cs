using System;
using System.Xml.Linq;
using System.Linq;

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
            if (Elements().Count() > 0)
            {
                var indent = GetIndentation(IndentationLevel);
                var stringBuilder = indent + Name.ToLower() + " " + Value + " {" + Environment.NewLine;
                stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                stringBuilder += indent + "}";
                return stringBuilder;
            }
            else
            {
                var indent = GetIndentation(IndentationLevel);
                return indent + Name.ToLower() + " " + Value.ToLower() + ";";
            }
        }
    }
}
