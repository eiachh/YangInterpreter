using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Rpc Statement RFC 6020 7.17.2. 
    /// 
    /// <summary>
    /// The "rpc" statement is used to define a NETCONF RPC operation. It
    /// takes one argument, which is an identifier, followed by a block of
    /// substatements that holds detailed rpc information.This argument is
    /// the name of the RPC, and is used as the element name directly under
    /// the <rpc> element, as designated by the substitution group
    /// "rpcOperation" in [RFC4741].
    /// </summary>
    public class RpcStatement : StatementBase
    {
        public RpcStatement() : base("rpc") { }
        public RpcStatement(string Argument) : base("rpc", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.RpcStatementAllowedSubstatements;
        }
    }
}
