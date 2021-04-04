using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Prefix Statement RFC 6020 7.1.4.
    /// 
    /// <summary>
    /// The "prefix" statement is used to define the prefix associated with
    /// the module and its namespace.The "prefix" statement’s argument is
    /// the prefix string that is used as a prefix to access a module.The
    /// prefix string MAY be used to refer to definitions contained in the
    /// module, e.g., "if:ifName". A prefix follows the same rules as an
    /// identifier (see Section 6.2).
    /// </summary>
    public class PrefixStatement : ChildlessStatement
    {
        internal override bool IsQuotedValue => true;
        public PrefixStatement() : base("prefix") { }
        public PrefixStatement(string Value) : base("prefix") { this.Value = Value; }

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
            var module = Root as ModuleStatement;
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
