using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    public class DerivedTypeStatement : TypeStatement
    {
        public DerivedTypeStatement(string Argument) : base(Argument) { BuiltInTypeOfNode = BuiltInTypes.derived; }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
