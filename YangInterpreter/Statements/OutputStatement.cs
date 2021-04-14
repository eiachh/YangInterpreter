using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Output Statement RFC 6020 7.13.3. 
    /// 
    /// <summary>
    /// The "output" statement, which is optional, is used to define output
    /// parameters to the RPC operation.It does not take an argument.The
    /// substatements to "output" define nodes under the RPC’s output node.
    /// </summary>
    public class OutputStatement : StatementBase
    {
        public OutputStatement() : base("output") { }
        public OutputStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.OutputStatementAllowedSubstatements;
        }
    }
}
