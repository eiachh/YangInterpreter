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
    /// uint8 represents integer values between 0 and 255, inclusively.
    /// </summary>
    public class UInt8TypeStatement : TypeStatement
    {
        public UInt8TypeStatement() : base(BuiltInTypes.uint8) { }
        public UInt8TypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.UInt8TypeStatementAllowedSubstatements;
        }
    }
}
