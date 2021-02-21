using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Error-App-Tag Statement RFC 6020 7.5.4.2. 
    ///
    /// <summary>
    /// The "error-app-tag" statement, which is optional, takes a string as
    /// an argument.If the constraint evaluates to false, the string is
    /// passed as <error-app-tag> in the<rpc-error>.
    /// </summary>
    public class ErrorAppTagStatement : StatementWithSingleValueBase
    {
        public ErrorAppTagStatement() : base("Error-App-Tag") { }
        public ErrorAppTagStatement(string Value) : base("Error-App-Tag",Value) { }
    }
}
