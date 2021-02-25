using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    /// <summary>
    /// This is the specialised class for "type bits { " inherited from the basic TypeStatement
    /// </summary>
    public class BitsTypeStatement : TypeStatement
    {
        public BitsTypeStatement() : base(BuiltInTypes.bits) { }
        public BitsTypeStatement(string Argument) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.BitTypeStatementAllowedSubstatements;
        }
    }
}
