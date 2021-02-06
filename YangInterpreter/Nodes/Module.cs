using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using YangInterpreter.Nodes.BaseNodes;
using YangInterpreter.Nodes;

namespace YangInterpreter
{
    public class Module : ContainerCapability
    {
        /// <summary>
        /// The Uri or Url of SELF namespace.
        /// </summary>
        public string Namespace { get; internal set; }
        /// <summary>
        /// The prefix for SELF namespace.faddchil
        /// </summary>
        public string Prefix { get; set; }
        public string Organization { get; set; }
        public string Contact { get; set; }

        public Module(string name) : base(name) { AddChild(new YangVersionNode("1")); }

        /// <summary>
        /// Namespace dictionary of imported modules. Keys are the prefixes, values are the full namespace.
        /// </summary>
        public Dictionary<string, string> NamespaceDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Adds the namespace to the dictionary. ( Prefix,  Namespace)
        /// </summary>
        /// <param name="_prefix"></param>
        /// <param name="_namespace"></param>
        public void AddNamespace(string _prefix, string _namespace)
        {
            if (!NamespaceDictionary.ContainsKey(_prefix))
                NamespaceDictionary.Add(_prefix, _namespace);
        }

        /// <summary>
        /// Returns full namespace based on prefix. If not found "Namespace not found" returned.
        /// </summary>
        /// <param name="_prefix"></param>
        /// <returns></returns>
        public string GetNamespace(string _prefix)
        {
            string outvalue = "Namespace not found!";
            NamespaceDictionary.TryGetValue(_prefix, out outvalue);
            return outvalue;
        }

        public override YangNode AddChild(YangNode Node)
        {
            if(Node.GetType() == typeof(YangVersionNode))
            {
                var version = DescendantsNode("yang-version")?.Single();
                if(version != null )
                    return version;
            }
            base.AddChild(Node);
            return Node;
        }
        public override string NodeAsYangString()
        {
            return NodeAsYangString(0);
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "module " + Name + " {" + Environment.NewLine;

            YangNode yangver = DescendantsNode("yang-version").Single();
            strBuilder += yangver.NodeAsYangString(indentationlevel + 1) + Environment.NewLine;

            strBuilder += GetSelfNamespaceWithPrefix(indentationlevel + 1);
            strBuilder += GetDictionaryAsYangString(indentationlevel + 1);
            strBuilder += GetOrganizationAsYangString(indentationlevel + 1);
            strBuilder += GetContactAsYangString(indentationlevel + 1);
            strBuilder += GetDescriptionAsYangString(indentationlevel + 1);
            strBuilder += GetReferenceAsYangString(indentationlevel + 1);

            strBuilder += GetChildrenAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }

        private string GetDictionaryAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            var strBuilder = "";
            if (NamespaceDictionary.Count == 0)
                return "";
            foreach (KeyValuePair<string, string> entry in NamespaceDictionary)
            {
                strBuilder += indent + "import " + entry.Value + " { prefix \"" + entry.Key + "\"; }" + Environment.NewLine;
            }
            return strBuilder + Environment.NewLine;
        }

        private string GetSelfNamespaceWithPrefix(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            string strBuilder = indent + "namespace" + Environment.NewLine;
            if (string.IsNullOrEmpty(Namespace))
                return "";
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1, Namespace)+ "\"" + Environment.NewLine + Environment.NewLine;
            strBuilder += indent + "prefix " + Prefix + ";" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }

        private string GetOrganizationAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            if (string.IsNullOrEmpty(Organization))
                return "";
            var strBuilder = indent + "organization" + Environment.NewLine;
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1, Organization)+ "\"" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }

        private string GetContactAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            if (string.IsNullOrEmpty(Contact))
                return "";
            var strBuilder = indent + "contact" + Environment.NewLine;
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1, Contact)+ "\"" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }

        private string GetDescriptionAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            var descValue = GetPropertyByName("description")?.Single().GetValue();
            if (descValue == null)
                return "";
            var strBuilder = indent + "description" + Environment.NewLine;
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1,descValue)+ "\"" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }

        private string GetReferenceAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            var refValue = GetPropertyByName("reference")?.Single().GetValue();
            if (refValue == null)
                return "";
            var strBuilder = indent + "reference" + Environment.NewLine;
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1, refValue)+ "\"" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }
    }
}
