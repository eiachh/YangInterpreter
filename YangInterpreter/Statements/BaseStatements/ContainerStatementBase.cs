using System;
using System.Xml.Linq;
using System.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
   /* public abstract class ContainerStatementBase : StatementBase
    {
        /// <summary>
        /// Defines if the value should be beetween quote symbols at toString()
        /// </summary>
        /*internal virtual bool IsQuotedValue { get; } = false;
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
        }*/

        
    //}
}
