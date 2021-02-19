using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    /// Leafref Built-in type RFC 6020 9.9.
    /// 
    /// <summary>
    /// The leafref type is used to reference a particular leaf instance in
    /// the data tree.The "path" substatement(Section 9.9.2) selects a set
    /// of leaf instances, and the leafref value space is the set of values
    /// of these leaf instances.
    /// </summary>
    public class LeafRefTypeStatement : TypeStatement
    {
        public LeafRefTypeStatement() : base(BuiltInTypes.leafref) { }
        public LeafRefTypeStatement(string Name) : this() { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.LeafRefTypeStatementAllowedSubstatements;
        }
    }
}
