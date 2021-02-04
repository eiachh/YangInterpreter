using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter.Nodes.Types
{
    class EmptyTypeNode : TypeNode
    {
        public EmptyTypeNode() : base(BuiltInTypes.empty) { }
        public EmptyTypeNode(string name) : this() { }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "type empty;";
        }
    }
}
