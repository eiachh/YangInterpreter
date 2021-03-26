using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Deviation Statement RFC 6020 7.18.3. 
    /// 
    /// <summary>
    /// The "deviation" statement defines a hierarchy of a module that the
    /// device does not implement faithfully.The argument is a string that
    /// identifies the node in the schema tree where a deviation from the
    /// module occurs.This node is called the deviation’s target node.The
    /// contents of the "deviation" statement give details about the
    /// deviation.
    /// </summary>
    /// +--------------+----------+-------------+
    /// | substatement | section  | cardinality |
    /// +--------------+----------+-------------+
    /// | description  | 7.19.3   | 0..1        |
    /// | deviate      | 7.18.3.2 | 1..n        |
    /// | reference    | 7.19.4   | 0..1        |
    /// +--------------+----------+-------------+
    /// 
    public class DeviationStatement : StatementBase
    {
        public DeviationStatement() : base("deviation") { }
        public DeviationStatement(string Argument) : base("deviation",Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.DeviationStatementAllowedSubstatements;
        }
    }
}
