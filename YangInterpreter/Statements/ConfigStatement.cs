using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Config Statement RFC 6020 7.19.1. 
    /// 
    /// <summary>
    /// The "config" statement takes as an argument the string "true" or
    /// "false". If "config" is "true", the definition represents
    /// configuration.Data nodes representing configuration will be part of
    /// the reply to a<get-config> request, and can be sent in a
    /// <copy-config> or <edit-config> request.
    /// </summary>
    public class ConfigStatement : ControlledValueChildlessStatement
    {
        public ConfigStatement() : base("config","true") { }
        public ConfigStatement(string Argument) : base("config",Argument) { }
        protected override string ImproperValueErrorMessage => "The given value can be true/false but was: " + Argument;

        protected override bool IsValidValue(string value)
        {
            return value == "false" || value == "true";
        }
    }
}
