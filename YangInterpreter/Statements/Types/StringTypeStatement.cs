using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    /// <summary>
    /// This is the specialised class for "type string { " inherited from the basic TypeStatement
    /// </summary>
    public class StringTypeStatement : TypeStatement
    {
        public StringTypeStatement() : base(BuiltInTypes.string_yang) { }
        public StringTypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.StringTypeStatementAllowedSubstatements;
        }
    }
}
