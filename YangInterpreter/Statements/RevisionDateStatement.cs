using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Revision-Date Statement RFC 6020 7.1.5.1. 
    /// 
    /// <summary>
    /// The import’s "revision-date" statement is used to specify the exact
    /// version of the module to import.The "revision-date" statement MUST
    /// match the most recent "revision" statement in the imported module.
    /// </summary>
    public class RevisionDateStatement : ChildlessStatement
    {
        public RevisionDateStatement() : base("revision-date") { }
        public RevisionDateStatement(string Argument) : base("revision-date", Argument) { }
    }
}
