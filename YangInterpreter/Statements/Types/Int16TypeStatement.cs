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
    /// int16 represents integer values between -32768 and 32767,
    /// inclusively.
    /// </summary>
    public class Int16TypeStatement : TypeStatement
    {
        public Int16TypeStatement() : base(BuiltInTypes.int16) { }
        public Int16TypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.Int16TypeStatementAllowedSubstatements;
        }
    }
}
