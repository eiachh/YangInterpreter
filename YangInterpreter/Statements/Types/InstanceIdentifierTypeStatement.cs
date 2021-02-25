using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Types
{
    /// instance-identifier Built-In Type RFC 6020 9.13
    ///
    /// <summary>
    /// The instance-identifier built-in type is used to uniquely identify a
    /// particular instance node in the data tree.
    /// </summary>
    public class InstanceIdentifierTypeStatement : TypeStatement
    {
        public InstanceIdentifierTypeStatement() : base(BuiltInTypes.instance_identifier) { }
        public InstanceIdentifierTypeStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.InstanceIdentifierTypeStatementAllowedSubstatements;
        }
    }
}
