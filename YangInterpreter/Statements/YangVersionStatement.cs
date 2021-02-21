using System;
using System.Collections.Generic;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Yang-Version Statement RFC 6020 7.1.2. 
    ///
    /// <summary>
    /// The optional "yang-version" statement specifies which version of the
    /// YANG language was used in developing the module.The statement’s
    /// argument is a string. If present, it MUST contain the value "1",
    /// which is the current YANG version and the default value.
    /// </summary>
    public class YangVersionStatement : ChildlessContainerStatement
    {
        public YangVersionStatement() : base("yang-version") { BuildIntoOutput = false; }
        public YangVersionStatement(string Value) : this() { base.Value = Value; }

        public override XElement[] StatementAsXML()
        {
            return new XElement[]{ new XElement("yang-version", base.Value) };
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + Name + " " + base.Value + ";";
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }

        internal override bool IsValueStartAtSameLine()
        {
            return true;
        }
    }
}
