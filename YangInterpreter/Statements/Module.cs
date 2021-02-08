using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements;

namespace YangInterpreter
{
    // RFC 6020 https://tools.ietf.org/pdf/rfc6020.pdf 7. page 39
    //Module can contain the following substatements
    /// +--------------+---------+-------------+
    ///| substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml      | 7.10    | 0..n  |
    /// | augment     | 7.15    | 0..n  |
    /// | choice      | 7.9     | 0..n  |
    /// | contact     | 7.1.8   | 0..1  |
    /// | container   | 7.5     | 0..n  |
    /// | description | 7.19.3  | 0..1  |
    /// | deviation   | 7.18.3  | 0..n  |
    /// | extension   | 7.17    | 0..n  |
    /// | feature     | 7.18.1  | 0..n  |
    /// | grouping    | 7.11    | 0..n  |
    /// | identity    | 7.16    | 0..n  |
    /// | import      | 7.1.5   | 0..n  |
    /// | include     | 7.1.6   | 0..n  |
    /// | leaf        | 7.6     | 0..n  |
    /// | leaf-list   | 7.7     | 0..n  |
    /// | list        | 7.8     | 0..n  |
    /// | namespace   | 7.1.3   | 1     |
    /// | notification | 7.14   | 0..n  |
    /// | organization | 7.1.7  | 0..1  |
    /// | prefix      | 7.1.4   | 1     |
    /// | reference   | 7.19.4  | 0..1  |
    /// | revision    | 7.1.9   | 0..n  |
    /// | rpc         | 7.13    | 0..n  |
    /// | typedef     | 7.3     | 0..n  |
    /// | uses        | 7.12    | 0..n  |
    /// | yang-version | 7.1.2  | 0..1  |
    ///+--------------+---------+-------------+
    public class Module : ContainerCapability
    {
        /// <summary>
        /// The prefix for SELF namespace.faddchil
        /// </summary>
        public string Prefix { get; set; }

        public Module(string name) : base(name) { AddStatement(new YangVersionNode("1")); }

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

        public override Statement AddStatement(Statement Node)
        {
            if (Node.GetType() == typeof(YangVersionNode))
            {
                var version = DescendantsNode("yang-version")?.Single();
                if (version != null)
                    return version;
            }
            base.AddStatement(Node);
            return Node;
        }

        public override string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "module " + Name + " {" + Environment.NewLine;

            Statement yangver = DescendantsNode("yang-version").Single();
            strBuilder += yangver.StatementAsYangString(indentationlevel + 1) + Environment.NewLine;

            strBuilder += GetStatementsAsYangString(indentationlevel + 1) + Environment.NewLine;
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


        /*private string GetDescriptionAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            var descValue = GetPropertyByName("description")?.Single().GetValue();
            if (descValue == null)
                return "";
            var strBuilder = indent + "description" + Environment.NewLine;
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1,descValue)+ "\"" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }

        /*private string GetReferenceAsYangString(int indentationLevel)
        {
            var indent = GetIndentation(indentationLevel);
            var refValue = GetPropertyByName("reference")?.Single().GetValue();
            if (refValue == null)
                return "";
            var strBuilder = indent + "reference" + Environment.NewLine;
            strBuilder += indent + "\t\"" + MultilineIndentFixer(indentationLevel + 1, refValue)+ "\"" + Environment.NewLine;
            return strBuilder + Environment.NewLine;
        }*/

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }
    }
}
