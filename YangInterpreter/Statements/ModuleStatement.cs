﻿using System;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter
{
    /// Module Statement RFC 6020 7.1.
    /// 
    /// The "module" statement defines the module’s name, and groups all
    /// statements that belong to the module together.The "module"
    /// statement’s argument is the name of the module, followed by a block
    /// of substatements that hold detailed module information.The module
    /// name follows the rules for identifiers in Section 6.2.
    /// Names of modules published in RFC streams [RFC4844] MUST be assigned
    /// by IANA, see Section 14.
    /// 
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | augment      | 7.15    | 0..n        |
    /// | choice       | 7.9     | 0..n        |
    /// | contact      | 7.1.8   | 0..1        |
    /// | container    | 7.5     | 0..n        |
    /// | description  | 7.19.3  | 0..1        |
    /// | deviation    | 7.18.3  | 0..n        |
    /// | extension    | 7.17    | 0..n        |
    /// | feature      | 7.18.1  | 0..n        |
    /// | grouping     | 7.11    | 0..n        |
    /// | identity     | 7.16    | 0..n        |
    /// | import       | 7.1.5   | 0..n        |
    /// | include      | 7.1.6   | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | namespace    | 7.1.3   | 1           |
    /// | notification | 7.14    | 0..n        |
    /// | organization | 7.1.7   | 0..1        |
    /// | prefix       | 7.1.4   | 1           |
    /// | reference    | 7.19.4  | 0..1        |
    /// | revision     | 7.1.9   | 0..n        |
    /// | rpc          | 7.13    | 0..n        |
    /// | typedef      | 7.3     | 0..n        |
    /// | uses         | 7.12    | 0..n        |
    /// | yang-version | 7.1.2   | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class ModuleStatement : StatementBase
    {
        /// <summary>
        /// The prefix for SELF namespace.faddchil
        /// </summary>
        public string Prefix { get; set; }
        public string Namespace { get; set; }
        public override StatementBase Parent { get => this;}

        public ModuleStatement() : base("module") { AddStatement(new YangVersionStatement("1")); }
        public ModuleStatement(string Value) : base("module",Value) { AddStatement(new YangVersionStatement("1")); }


        /// <summary>
        /// Namespace dictionary of imported modules. Keys are the prefixes, values are the full namespace.
        /// </summary>
        public Dictionary<string, string> NamespaceDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Adds the namespace to the dictionary. ( Prefix,  Namespace)
        /// </summary>
        /// <param name="_prefix"></param>
        /// <param name="_namespace"></param>
        internal void AddNamespace(string _prefix, string _namespace)
        {
            if (!NamespaceDictionary.ContainsKey(_prefix))
                NamespaceDictionary.Add(_prefix, _namespace);
        }

        /// <summary>
        /// Returns full namespace based on prefix. If not found empty string is returned.
        /// </summary>
        /// <param name="_prefix"></param>
        /// <returns></returns>
        public string GetNamespace(string _prefix)
        {
            string outvalue = "";
            NamespaceDictionary.TryGetValue(_prefix, out outvalue);
            return outvalue;
        }

        /// <summary>
        /// Returns the prefix for the given NameSpace, null if not found.
        /// </summary>
        /// <param name="NameSpace"></param>
        /// <returns></returns>
        public string GetPrefixByNamespace(string NameSpace)
        {
            foreach (var entry in NamespaceDictionary)
            {
                if (entry.Value == NameSpace)
                    return entry.Key;
            }
            return "";
        }

        public override StatementBase AddStatement(StatementBase Node)
        {
            if (Root == null)
                Root = this;
            if (Node.GetType() == typeof(YangVersionStatement))
            {
                var version = Descendants("yang-version")?.Single();
                if (version != null)
                    return version;
            }
            else if(Node.GetType() == typeof(PrefixStatement))
            {
                Prefix = Node.Argument;
            }
            else if (Node.GetType() == typeof(NamespaceStatement))
            {
                Namespace = Node.Argument;
            }
            base.AddStatement(Node);
            return Node;
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ModuleStatementAllowedSubstatements;
        }
    }
}
