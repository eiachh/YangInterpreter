﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.Property;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter
{
    public class Leaf : ContainerStatementBase
    {
        //public bool Config { get; set; }

        public Leaf() : base("Leaf") { }
        public Leaf(string Value) : base("Leaf", Value) { }


        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.LeafStatementAllowedSubstatements;
        }
    }
}
