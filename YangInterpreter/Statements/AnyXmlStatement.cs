using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// AnyXml Statement RFC 6020 7.10. 
    /// 
    /// <summary>
    /// The "anyxml" statement defines an interior node in the schema tree.
    /// It takes one argument, which is an identifier, followed by a block of
    /// substatements that holds detailed anyxml information.
    /// </summary>
    public class AnyXmlStatement : StatementBase
    {
        public AnyXmlStatement() : base("AnyXml") { }
        public AnyXmlStatement(string Argument) : base("AnyXml", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.AnyXmlStatementAllowedSubstatements;
        }
    }
}
