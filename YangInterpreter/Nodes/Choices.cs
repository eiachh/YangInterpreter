using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Nodes.BaseNodes;
using System.Xml.Linq;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Nodes
{
    public class Choices : ContainerCapability
    { 
        public Choices(string name) : base(name){ }
        public override YangNode AddChild(YangNode cases)
        {
            if (cases.GetType() != typeof(ChoiceCase))
            {
                throw new TypeMissmatch("You are trying to set a YangNode as child for Choices, which is not a Choicecase. Make sure that choices only contain case node!");
            }
            else
            {
                base.AddChild(cases);
            }
            return cases;
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "choice " + Name + " {" + Environment.NewLine;
            strBuilder += GetChildrenAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }

        public override string NodeAsYangString()
        {
            return NodeAsYangString(0);
        }

        public XElement[] NodeAsXmlForUses()
        {
            List<XElement> retchildlist = new List<XElement>();
            foreach (var child in Children)
            {
                retchildlist.AddRange(child.NodeAsXML());
            }
            return retchildlist.ToArray();
        }
    }
}
