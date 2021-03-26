using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Feature Statement RFC 6020 7.9.
    /// 
    /// <summary>
    /// The "feature" statement is used to define a mechanism by which
    /// portions of the schema are marked as conditional.A feature name is
    /// defined that can later be referenced using the "if-feature" statement
    /// (see Section 7.18.2). Schema nodes tagged with a feature are ignored
    /// by the device unless the device supports the given feature.This
    /// allows portions of the YANG module to be conditional based on
    /// conditions on the device.The model can represent the abilities of
    /// the device within the model, giving a richer model that allows for
    /// differing device abilities and roles.
    /// </summary>
    /// 
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | status       | 7.19.2  | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class FeatureStatement : StatementBase
    {
        public FeatureStatement() : base("feature") { }
        public FeatureStatement(string Argument) : base("feature", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.FeatureStatementAllowedSubstatements;
        }
    }
}
