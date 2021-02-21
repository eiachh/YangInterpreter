using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Uses : ContainerStatementBase
    {
        public Uses(string name) : base(name) { ContainedGrouping = Grouping.GetGroupingByName(name,this); }
        public Grouping ContainedGrouping { get; set; }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
