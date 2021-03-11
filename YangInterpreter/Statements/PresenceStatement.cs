using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Presence Statement RFC 6020 7.5.5. 
    /// 
    /// <summary>
    /// The "presence" statement assigns a meaning to the presence of a
    /// container in the data tree.It takes as an argument a string that
    /// contains a textual description of what the node’s presence means.
    /// </summary>
    public class PresenceStatement : ChildlessStatement
    {
        public PresenceStatement() : base("presence") { }
        public PresenceStatement(string Argument) : base("presence", Argument) { }
    }
}
