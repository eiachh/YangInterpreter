using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Types
{
    /// Identityref built-in-type RFC 6020 9.10.
    /// 
    /// <summary>
    /// The identityref type is used to reference an existing identity (see
    /// Section 7.16).
    /// </summary>
    public class IdentityrefTypeStatement : TypeStatement
    {
        public IdentityrefTypeStatement() : base(BuiltInTypes.identityref) { }
        public IdentityrefTypeStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.IdentityrefTypeStatementAllowedSubstatements;
        }
    }
}
