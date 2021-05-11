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
        public PrefixStatement(string Value) : base("prefix") { this.Argument = Value; }

        public override string Argument
        {
            get => base.Argument;
            set
            {
                HandleValueChange(Argument, value);
                base.Argument = value;
            }
        }

        public override StatementBase Parent
        {
            get => base.Parent;
            internal set
            {
                base.Parent = value;
                HandleValueChange(null, Argument);
            }
        }

        internal override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "prefix " + Argument + ";";
        }

        private void HandleValueChange(string originalValueOfPrefix, string newValueOfPrefix)
        {
            var module = Root as ModuleStatement;
            if (!string.IsNullOrEmpty(originalValueOfPrefix))
            {
                var oldValueForPrefixKey = module.NamespaceDictionary[originalValueOfPrefix];
                module.NamespaceDictionary.Remove(originalValueOfPrefix);
                module.AddNamespace(newValueOfPrefix, oldValueForPrefixKey);
            }
            else
            {
                if (Parent != null)
                    module.AddNamespace(newValueOfPrefix, Parent.Argument);
            }
        }
    }
}
