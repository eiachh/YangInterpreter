using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Argument Statement RFC 6020 7.17.2. 
    /// 
    /// <summary>
    /// The "argument" statement, which is optional, takes as an argument a
    /// string that is the name of the argument to the keyword.If no
    /// argument statement is present, the keyword expects no argument when
    /// it is used.
    /// </summary>
    /// 
    /// +--------------+----------+-------------+
    /// | substatement | section  | cardinality |
    /// +--------------+----------+-------------+
    /// | yin-element  | 7.17.2.2 | 0..1        |
    /// +--------------+----------+-------------+
    /// 
    public class ArgumentStatement : StatementBase
    {
        public ArgumentStatement() : base("argument") { }
        public ArgumentStatement(string Argument) : base("argument", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ArgumentStatementAllowedSubstatements;
        }
    }
}
