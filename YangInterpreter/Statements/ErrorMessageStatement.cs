using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Erroe-Message Statement RFC 6020 7.5.4.1
    /// 
    /// <summary>
    /// The "error-message" statement, which is optional, takes a string as
    /// an argument.If the constraint evaluates to false, the string is
    /// passed as <error-message> in the<rpc-error>.
    /// </summary>
    public class ErrorMessageStatement : StatementWithSingleValueBase
    {
        public ErrorMessageStatement() : base("Error-Message") { }
        public ErrorMessageStatement(string Value) : base("Error-Message",Value) { }
    }
}
