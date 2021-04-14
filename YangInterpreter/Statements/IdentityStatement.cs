using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Identity Statement RFC 6020 7.16.1. 
    /// 
    /// <summary>
    /// The "identity" statement is used to define a new globally unique,
    /// abstract, and untyped identity.Its only purpose is to denote its
    /// name, semantics, and existence.An identity can either be defined
    /// from scratch or derived from a base identity.The identity’s
    /// argument is an identifier that is the name of the identity.It is
    /// followed by a block of substatements that holds detailed identity
    /// information.
    /// </summary>
    /// 
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | base         | 7.16.2  | 0..1        |
    /// | description  | 7.19.3  | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// +--------------+---------+-------------+
    /// 

    public class IdentityStatement : StatementBase
    {
        public IdentityStatement() : base("identity") { }
        public IdentityStatement(string Argument) : base("identity", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.IdentityStatementAllowedSubstatements;
        }
    }
}
