﻿using System;
using System.Collections.Generic;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class ChildlessStatement : StatementBase
    {

        public ChildlessStatement(string Name) : base(Name) { }
        public ChildlessStatement(string Name, string Value) : base(Name) { base.Argument = Value; }

        internal override bool IsQuotedValue => true;

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
