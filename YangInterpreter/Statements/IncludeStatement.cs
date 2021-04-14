using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Include Statement RFC 6020 7.1.6. 
    /// 
    /// <summary>
    /// The "include" statement is used to make content from a submodule
    /// available to that submodule’s parent module, or to another submodule
    /// of that parent module.The argument is an identifier that is the
    /// name of the submodule to include.Modules are only allowed to
    /// include submodules that belong to that module, as defined by the
    /// "belongs-to" statement (see Section 7.2.2). Submodules are only
    /// allowed to include other submodules belonging to the same module.
    /// </summary>
    /// 
    /// +---------------+---------+-------------+
    /// | substatement  | section | cardinality |
    /// +---------------+---------+-------------+
    /// | revision-date | 7.1.5.1 | 0..1        |
    /// +---------------+---------+-------------+
    /// 

    public class IncludeStatement : StatementBase
    {
        public IncludeStatement() : base("include") { }
        public IncludeStatement(string Argument) : base("include", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.IncludeStatementAllowedSubstatements;
        }
    }
}
