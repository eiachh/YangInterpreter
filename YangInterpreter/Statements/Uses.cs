using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Uses : BaseStatement
    {
        public Uses(string name) : base(name) { ContainedGrouping = Grouping.GetGroupingByName(name,this); }
        public Grouping ContainedGrouping { get; set; }
        public override XElement[] StatementAsXML()
        {
            return ContainedGrouping.NodeAsXmlForUses();
        }

        public override string StatementAsYangString(int identationlevel)
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString()
        {
            throw new NotImplementedException();
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
