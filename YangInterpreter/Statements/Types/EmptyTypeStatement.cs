using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    public class EmptyTypeStatement : TypeStatement
    {
        public EmptyTypeStatement() : base(BuiltInTypes.empty) { }
        public EmptyTypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
