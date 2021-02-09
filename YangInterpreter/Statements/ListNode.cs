using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter
{
    public class ListNode : Statement
    {
        /// <summary>
        /// The key property tells which Leaf node(s) of this list is(are) the key(s).
        /// </summary>
        public string Key { get; set; }
        public ListNode(string name) : base(name) { }

        public override string StatementAsYangString()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int identationlevel)
        {
            throw new NotImplementedException();
        }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }
    }
}
