using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Contact Statement RFC 6020 7.1.8. 
    /// 
    /// <summary>
    /// The "contact" statement provides contact information for the module.
    /// The argument is a string that is used to specify contact information
    /// for the person or persons to whom technical queries concerning this
    /// module should be sent, such as their name, postal address, telephone
    /// number, and electronic mail address
    /// </summary>
    public class ContactStatement : ChildlessStatement
    {
        public ContactStatement() : base("contact") { }
        public ContactStatement(string Value) : base("contact", Value) { }
        internal override bool IsQuotedValue => true;
    }
}
