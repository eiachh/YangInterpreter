using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    /// Integer Built-in-Types RFC 6020 9.2
    ///
    /// <summary>
    /// The integer built-in types are int8, int16, int32, int64, uint8,
    /// uint16, uint32, and uint64.They represent signed and unsigned
    /// integers of different sizes:
    /// int64 represents integer values between -9223372036854775808 and
    /// 9223372036854775807, inclusively.
    /// </summary>
    public class Int64TypeStatement : TypeStatement
    {
        public Int64TypeStatement() : base(BuiltInTypes.int64) { }
        public Int64TypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.Int64TypeStatementAllowedSubstatements;
        }
    }
}
