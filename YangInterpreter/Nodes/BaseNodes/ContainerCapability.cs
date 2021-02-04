using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YangInterpreter.Nodes.BaseNodes
{
    public abstract class ContainerCapability : YangNode
    {
        protected List<YangNode> Children = new List<YangNode>();
        protected ContainerCapability(string name) : base(name) { }

        /// <summary>
        /// Adds the given Node as child to this node returns the added Node.
        /// </summary>
        /// <param name="Node"></param>
        /// <returns>Returns the added Node can be used to check if adding changed any value</returns>
        public virtual YangNode AddChild(YangNode Node)
        {
            Children.Add(Node);
            Node.Parent = this;
            return Node;
        }

        /// <summary>
        /// Returns the first child element.
        /// </summary>
        public YangNode FirstChild()
        {
            if (Children.Count > 0)
            {
                return Children[0];
            }
            else
            {
                return null;
            }
        }
        public YangNode LastChild()
        {
            if (Children.Count > 0)
            {
                return Children[Children.Count - 1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the amount of children immadiately after this node.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Children.Count;
        }

        public override XElement[] NodeAsXML()
        {
            XElement containerasroot = new XElement(this.Name);

            foreach (var child in Children)
            {
                if (child.NodeAsXML() != null)
                {
                    containerasroot.Add(child.NodeAsXML());
                }
            }
            return new XElement[] { containerasroot };
        }

        protected string GetChildrenAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            var strBuilder = "";
            foreach (var child in Children)
            {
                if(child.BuildIntoOutput)
                    strBuilder += child.NodeAsYangString(indentationLevel) + Environment.NewLine;
            }
            return strBuilder;
        }

        /// <summary>
        /// Looks for the Node with the given name in any depth any child Node.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<YangNode> DescendantsNode(string Name)
        {
            List<YangNode> MatchingElements = new List<YangNode>();
            bool hasAny = false;
            foreach (var child in Children)
            {
                if (child.GetType().IsInstanceOfType(typeof(ContainerCapability)))
                {
                    var Descendants = ((ContainerCapability)child).DescendantsNode(Name);
                    if(Descendants != null)
                    {
                        hasAny = true;
                        MatchingElements.AddRange(Descendants);
                    }
                }
                if (child.Name.ToLower().Contains(Name.ToLower()))
                {
                    hasAny = true;
                    MatchingElements.Add(child);
                } 
            }
            if (hasAny)
                return MatchingElements;
            else
                return null;
        }
    }
}
