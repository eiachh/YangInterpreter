using System;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Import Statement RFC 6020 7.1.5.
    ///
    /// <summary>
    /// +---------------+---------+-------------+
    /// | substatement  | section | cardinality |
    /// +---------------+---------+-------------+
    /// | prefix        | 7.1.4   | 1           |
    /// | revision-date | 7.1.5.1 | 0..1        |
    /// +---------------+---------+-------------+
    /// </summary>
    public class ImportStatement : StatementBase
    {
        public ImportStatement() : base("import") { }
        public ImportStatement(string Value) : base("import", Value) { }

        public override string Argument 
        { 
            get => base.Argument;
            set
            {
                HandleValueChange(value);
                base.Argument = value;
            }
        }

        internal override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + Name.ToLower() + " " + Argument + " { " + GetStatementsAsYangString(0) + " }";
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ImportStatementAllowedSubstatements;
        }

        /// <summary>
        /// Changes Value for this namespace in module`s dictionary.
        /// </summary>
        /// <param name="newValueOfPrefix"></param>
        private void HandleValueChange(string newValueOfPrefix)
        {
            var module = Root as ModuleStatement;
            var childPrefix = Descendants("prefix");

            if (childPrefix is null)
                return;

            string key = childPrefix.Single().Argument;
            module.NamespaceDictionary[key] = newValueOfPrefix;
        }
    }
}
