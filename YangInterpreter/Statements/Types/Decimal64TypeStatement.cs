using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    /// Decimal Built-in-Types RFC 6020 9.3
    ///
    /// <summary>
    /// The decimal64 type represents a subset of the real numbers, which can
    /// be represented by decimal numerals.The value space of decimal64 is
    /// the set of numbers that can be obtained by multiplying a 64-bit
    /// signed integer by a negative power of ten, i.e., expressible as
    /// "i x 10^-n" where i is an integer64 and n is an integer between 1 and
    /// 18, inclusively.
    /// </summary>
    public class Decimal64TypeStatement : TypeStatement
    {
        public Decimal64TypeStatement() : base(BuiltInTypes.decimal64) { }
        public Decimal64TypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.Decimal64TypeStatementAllowedSubstatements;
        }
    }
}
