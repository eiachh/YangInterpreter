﻿using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Choice`s Case Statement RFC 6020 7.9.2. 
    /// 
    /// <summary>
    /// The "case" statement is used to define branches of the choice. It
    /// takes as an argument an identifier, followed by a block of
    /// substatements that holds detailed case information.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | choice       | 7.9     | 0..n        |
    /// | container    | 7.5     | 0..n        |
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | uses         | 7.12    | 0..n        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    ///
    public class ChoiceCaseStatement : StatementBase
    {
        public ChoiceCaseStatement() : base("case") { }
        public ChoiceCaseStatement(string Argument) : base("case", Argument) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ChoiceCaseStatementAllowedSubstatements;
        }
    }
}
