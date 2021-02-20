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
    /// int32 represents integer values between -2147483648 and 2147483647,
    /// inclusively.
    /// </summary>
    public class Int32TypeStatement : TypeStatement
    {
        public Int32TypeStatement() : base(BuiltInTypes.int32) { }
        public Int32TypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.Int32TypeStatementAllowedSubstatements;
        }
    }
}
