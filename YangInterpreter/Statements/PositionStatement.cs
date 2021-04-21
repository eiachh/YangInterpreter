using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Position Statement RFC 6020 9.7.4.2. 
    /// 
    /// <summary>
    /// The "position" statement, which is optional, takes as an argument a
    /// non-negative integer value that specifies the bit’s position within a
    /// hypothetical bit field.The position value MUST be in the range 0 to
    /// 4294967295, and it MUST be unique within the bits type.The value is
    /// unused by YANG and the NETCONF messages, but is carried as a
    /// convenience to implementors.
    /// </summary>
    public class PositionStatement : ChildlessStatement
    {
        internal override bool IsQuotedValue => true;
        public PositionStatement(string Value) : base("Position",Value) { }

        internal override bool IsValueStartAtSameLine() { return true; }
    }
}
