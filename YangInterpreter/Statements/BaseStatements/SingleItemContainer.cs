using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class SingleItemContainer : Statement
    {
        protected SingleItemContainer(string name) : base(name) { }
        public override Statement AddStatement(Statement Node)
        {
            if (StatementList.Count == 0)
            {
                StatementList.Add(Node);
                Node.Parent = this;
            }
            else
            {
                throw new OverflownContainer("Container cannot contain more than 1 child. Container overflown.");
            }
            return Node;
        }
        public void SetChild(Statement Node)
        {
            StatementList.Clear();
            StatementList.Add(Node);
        }
        public Statement GetChild()
        {
            return FirstChild();
        }
    }
}
