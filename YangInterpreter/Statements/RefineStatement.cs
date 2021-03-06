using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Refine Statement RFC 6020 7.12.2.
    /// 
    /// <summary>
    /// Some of the properties of each node in the grouping can be refined
    /// with the "refine" statement.The argument is a string that
    /// identifies a node in the grouping.This node is called the refine’s
    /// target node.If a node in the grouping is not present as a target
    /// node of a "refine" statement, it is not refined, and thus used
    /// exactly as it was defined in the grouping.
    /// </summary>
    public class RefineStatement : ChildlessStatement
    {
        public RefineStatement() : base("refine") { }
        public RefineStatement(string Argument) : base("refine", Argument) { }
    }
}
