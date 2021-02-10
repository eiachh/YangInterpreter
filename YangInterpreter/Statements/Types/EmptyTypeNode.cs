using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    class EmptyTypeNode : TypeNode
    {
        public EmptyTypeNode() : base(BuiltInTypes.empty) { }
        public EmptyTypeNode(string name) : this() { }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "type empty;";
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }
    }
}
