using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using System.Linq;

namespace YangInterpreter.Statements
{
    public class Import : Statement
    {
        public Import() : base("Import") { }
        public Import(string _Value) : base("Import")
        {
            Value = _Value;
        }

        public override string Value 
        { 
            get => base.Value;
            set
            {
                HandleValueChange(value);
                base.Value = value;
            }
        }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + Name.ToLower() + " " + Value + " { " + GetStatementsAsYangString(0) + " }";
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }

        /// <summary>
        /// Changes Value for this namespace in module`s dictionary.
        /// </summary>
        /// <param name="newValueOfPrefix"></param>
        private void HandleValueChange(string newValueOfPrefix)
        {
            var module = Root as Module;
            var childPrefix = Descendants("prefif");

            if (childPrefix is null)
                return;

            string key = childPrefix.Single().Value;
            module.NamespaceDictionary[key] = newValueOfPrefix;
        }
    }
}
