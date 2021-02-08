using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class ContainerCapability : Statement
    {
        protected ContainerCapability(string name) : base(name) { }

        /// <summary>
        /// Adds the given Node as child to this node returns the added Node.
        /// </summary>
        /// <param name="Node"></param>
        /// <returns>Returns the added Node can be used to check if adding changed any value</returns>
        /*public virtual Statement AddSubstatement(Statement Node)
        {
            SubStatements.Add(Node);
            Node.Parent = this;
            return Node;
        }*/

        /// <summary>
        /// Returns the first child element.
        /// </summary>
        /*public Statement FirstChild()
        {
            if (StatementList.Count > 0)
            {
                return StatementList[0];
            }
            else
            {
                return null;
            }
        }
        /*public Statement LastChild()
        {
            if (StatementList.Count > 0)
            {
                return StatementList[StatementList.Count - 1];
            }
            else
            {
                return null;
            }
        }*/

        /// <summary>
        /// Returns the amount of children immadiately under this node.
        /// </summary>
        /// <returns></returns>
        /*public int Count()
        {
            return StatementList.Count;
        }*/

        /*public override XElement[] NodeAsXML()
        {
            XElement containerasroot = new XElement(this.Name);

            foreach (var child in StatementList)
            {
                if (child.NodeAsXML() != null)
                {
                    containerasroot.Add(child.NodeAsXML());
                }
            }
            return new XElement[] { containerasroot };
        }*/

        /// <summary>
        /// Returns the substatement nodes as string properly formatted for YANG syntax.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <returns></returns>
        /*protected string GetStatementsAsYangString(int indentationLevel)
        {
            var strBuilder = "";
            foreach (var child in StatementList)
            {
                if (child.BuildIntoOutput)
                    strBuilder += child.StatementAsYangString(indentationLevel) + Environment.NewLine;
            }
            return strBuilder;
        }*/

        /// <summary>
        /// Returns the substatement nodes as string properly formatted for YANG syntax.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <returns></returns>
        /*protected string GetSubStatementsAsYangString(int indentationLevel)
        {
            var strBuilder = "";
            foreach (var child in StatementList)
            {
                if(child.BuildIntoOutput)
                    strBuilder += child.StatementAsYangString(indentationLevel) + Environment.NewLine;
            }
            return strBuilder;
        }*/

        /// <summary>
        /// Looks for the Node with the given name in any depth any child Node.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /*public IEnumerable<Statement> DescendantsNode(string Name)
        {
            List<Statement> MatchingElements = new List<Statement>();
            bool hasAny = false;
            foreach (var child in StatementList)
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
        }*/
    }
}
