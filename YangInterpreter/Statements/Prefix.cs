using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    public class Prefix : ChildlessContainerStatement
    {
        internal override bool IsQuotedValue => true;
        public Prefix() : base("Prefix") { }
        public Prefix(string Value) : base("Prefix") { this.Value = Value; }

        public override string Value
        {
            get => base.Value;
            set
            {
                HandleValueChange(Value, value);
                base.Value = value;
            }
        }

        public override StatementBase Parent
        {
            get => base.Parent;
            set
            {
                base.Parent = value;
                HandleValueChange(null, Value);
            }
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "prefix " + Value + ";";
        }

        private void HandleValueChange(string originalValueOfPrefix, string newValueOfPrefix)
        {
            var module = Root as Module;
            if (!string.IsNullOrEmpty(originalValueOfPrefix))
            {
                var oldValueForPrefixKey = module.NamespaceDictionary[originalValueOfPrefix];
                module.NamespaceDictionary.Remove(originalValueOfPrefix);
                module.NamespaceDictionary.Add(newValueOfPrefix, oldValueForPrefixKey);
            }
            else
            {
                if (Parent != null)
                    module.NamespaceDictionary.Add(newValueOfPrefix, Parent.Value);
            }
        }
    }
}
