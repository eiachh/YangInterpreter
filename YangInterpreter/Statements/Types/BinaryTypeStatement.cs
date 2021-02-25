using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Types
{
    /// binary-built-in-type  RFC 6020 9.8. 
    /// 
    /// <summary>
    /// The binary built-in type represents any binary data, i.e., a sequence
    /// of octets.
    /// </summary>
    public class BinaryTypeStatement : TypeStatement
    {
        public BinaryTypeStatement() : base(BuiltInTypes.binary) { }
        public BinaryTypeStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.BinaryTypeStatementAllowedSubstatements;
        }
    }
}
