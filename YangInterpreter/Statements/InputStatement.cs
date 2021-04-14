using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Input Statement RFC 6020 7.13.2. 
    /// 
    /// <summary>
    /// The "input" statement, which is optional, is used to define input
    /// parameters to the RPC operation.It does not take an argument.The
    /// substatements to "input" define nodes under the RPC’s input node.
    /// </summary>
    public class InputStatement : StatementBase
    {
        public InputStatement() : base("input") { }
        public InputStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.InputStatementAllowedSubstatements;
        }
    }
}
