using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Extension Statement RFC 6020 7.17. 
    /// 
    /// <summary>
    /// The "extension" statement allows the definition of new statements
    /// within the YANG language.This new statement definition can be
    /// imported and used by other modules.
    /// </summary>
    public class ExtensionStatement : StatementBase
    {
        public ExtensionStatement() : base("extension") { }
        public ExtensionStatement(string Argument) : base("extension", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ExtensionStatementAllowedSubstatements;
        }
    }
}
