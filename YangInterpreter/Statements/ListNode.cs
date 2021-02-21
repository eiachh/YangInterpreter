using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter
{
    public class ListNode : ContainerStatementBase
    {
        /// <summary>
        /// The key property tells which Leaf node(s) of this list is(are) the key(s).
        /// </summary>
        public string Key { get; set; }
        public ListNode(string Value) : base("List",Value) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
