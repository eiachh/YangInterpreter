using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// MUST Statement RFC 6020 7.6.5. 
    /// 
    /// <summary>
    /// The "must" statement, which is optional, takes as an argument a
    /// string that contains an XPath expression(see Section 6.4). It is
    /// used to formally declare a constraint on valid data.The constraint
    /// is enforced according to the rules in Section 8.
    /// </summary>
    public class MustStatement : StatementBase
    {
        public MustStatement() : base("must") { }
        public MustStatement(string Argument) : base("must",Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.MustStatementAllowedSubstatements;
        }
    }
}
