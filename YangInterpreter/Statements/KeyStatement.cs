using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// List`s Key Statement RFC 6020 7.8.2
    /// 
    /// <summary>
    /// The "key" statement, which MUST be present if the list represents
    /// configuration, and MAY be present otherwise, takes as an argument a
    /// string that specifies a space-separated list of leaf identifiers of
    /// this list.A leaf identifier MUST NOT appear more than once in the
    /// key. Each such leaf identifier MUST refer to a child leaf of the
    /// list.The leafs can be defined directly in substatements to the
    /// list, or in groupings used in the list.
    /// </summary>
    public class KeyStatement : ChildlessStatement
    {
        public KeyStatement() : base("key") { }
        public KeyStatement(string Argument) : base("key", Argument) { }
    }
}
