﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Property
{
    public class Prefix : Statement
    {
        public Prefix() : base("Prefix") { }
        public Prefix(string _Value) : base("Prefix")
        {
            Value = _Value;
        }

        public override string Value
        {
            get => base.Value;
            set
            {
                HandleValueChange(base.Value, value);
                base.Value = value;
            }
        }

        public override Statement Parent 
        { 
            get => base.Parent;
            set
            {
                base.Parent = value;
            }
        }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "prefix " + Value + ";";
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }

        private void HandleValueChange(string originalValueOfPrefix,string newValueOfPrefix)
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
                if(Parent != null)
                    if(Parent.GetType() != typeof(Module))
                        module.NamespaceDictionary.Add(newValueOfPrefix, Parent.Value);
                    else
                        module.NamespaceDictionary.Add(newValueOfPrefix, Parent.Name);
            }
                
        }
    }
}
