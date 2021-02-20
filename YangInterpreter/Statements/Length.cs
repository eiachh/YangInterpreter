using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Length : ControlledSingleValueBase
    {
        public Length() : base("Length") { }
        public Length(string Value) : base("Length",Value) { }

        protected override string ImproperValueErrorMessage { get => "The given value: "+ Value + " is not valid for Length statement!"; }

        protected override bool IsValidValue(string value)
        {
            value = value.Replace("\r\n", "").Replace("\n", "");
            return new Regex("^(?:[0-9]+\\.\\.[0-9]+)(?:\\s?\\|\\s?(?:[0-9]*\\.\\.[0-9]*))*$").Match(value).Success;
        }

        internal override bool IsValueStartAtSameLine()
        {
            return true;
        }
    }
}
