using System;
using System.Xml.Linq;
using System.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class ContainerStatementBase : BaseStatement
    {
        /// <summary>
        /// Defines if the value should be beetween quote symbols at toString()
        /// </summary>
        internal virtual bool IsQuotedValue { get; } = false;
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
                if (!IsQuotedValue)
                {
                    var indent = GetIndentation(IndentationLevel);
                    var stringBuilder = indent + Name.ToLower() + " " + Value + " {" + Environment.NewLine;
                    stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                    stringBuilder += indent + "}";
                    return stringBuilder;
                }
                else
                {
                    if (IsValueStartAtSameLine())
                    {
                        var indent = GetIndentation(IndentationLevel);
                        var stringBuilder = indent + Name.ToLower() + " \"" + MultilineIndentFixer(IndentationLevel + 1, Value) + "\" {" + Environment.NewLine;
                        stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                        stringBuilder += indent + "}";
                        return stringBuilder;
                    }
                    else
                    {
                        var indent = GetIndentation(IndentationLevel);
                        var stringBuilder = indent + Name.ToLower() + Environment.NewLine + "\t" + "\"" + MultilineIndentFixer(IndentationLevel + 1, Value) + "\" {" + Environment.NewLine;
                        stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                        stringBuilder += indent + "}";
                        return stringBuilder;
                    }
                    
                }
            }
            else
            {
                if(!IsQuotedValue)
                {
                    var indent = GetIndentation(IndentationLevel);
                    return indent + Name.ToLower() + " " + Value.ToLower() + ";";
                }
                else
                {
                    if (IsValueStartAtSameLine())
                    {
                        var indent = GetIndentation(IndentationLevel);
                        return indent + Name.ToLower() + " \"" + MultilineIndentFixer(IndentationLevel + 1, Value) + "\";";
                    }
                    else
                    {
                        var indent = GetIndentation(IndentationLevel);
                        return indent + Name.ToLower() + Environment.NewLine + "\t" + "\"" + MultilineIndentFixer(IndentationLevel + 1, Value) + "\";";
                    }
                }
            }
        }
    }
}
