﻿using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;
using System.Collections.Generic;
using System;

namespace YangInterpreter.Statements
{
    /// Organization statement RFC 6020  7.1.7.
    ///
    /// <summary>
    /// The "organization" statement defines the party responsible for this
    /// module.The argument is a string that is used to specify a textual
    /// description of the organization(s) under whose auspices this module
    /// was developed.
    ///</summary>
    public class Organization : ChildlessStatement
    {
        internal override bool IsQuotedValue => true;
        public Organization() : base("Organization") { }
        public Organization(string Value) : base("Organization") { base.Value = Value; }
    }
}
