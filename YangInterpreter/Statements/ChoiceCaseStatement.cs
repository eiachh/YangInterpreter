using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Choice`s Case Statement RFC 6020 7.9.2. 
    /// 
    /// <summary>
    /// The "case" statement is used to define branches of the choice. It
    /// takes as an argument an identifier, followed by a block of
    /// substatements that holds detailed case information.
    /// </summary>
    public class ChoiceCaseStatement : StatementBase
    {
        public ChoiceCaseStatement() : base("case") { }
        public ChoiceCaseStatement(string Argument) : base("case", Argument) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
