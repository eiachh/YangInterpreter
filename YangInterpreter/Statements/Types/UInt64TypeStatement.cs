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
    /// uint64 represents integer values between 0 and 18446744073709551615,
    /// inclusively.
    /// </summary>
    public class UInt64TypeStatement : TypeStatement
    {
        public UInt64TypeStatement() : base(BuiltInTypes.uint64) { }
        public UInt64TypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.UInt64TypeStatementAllowedSubstatements;
        }
    }
}
