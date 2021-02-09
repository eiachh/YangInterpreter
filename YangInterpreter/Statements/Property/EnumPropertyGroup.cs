using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Property
{
    internal class EnumPropertyGroup : Statement
    {
        public List<EnumProperty> EnumList = new List<EnumProperty>();

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString()
        {
            string retVal = "";
            foreach (var enumItem in EnumList)
            {
                retVal += enumItem.StatementAsYangString() + "\r\n";
            }
            return retVal;
        }

        /*public override string StatementAsYangText(int indentationlevel)
        {
            string indent = GetIdentation(indentationlevel);
            string PropAsTextBasic = StatementAsYangString().Trim();


            PropAsTextBasic = indent + PropAsTextBasic;
            return PropAsTextBasic.Replace("\r\n", "\r\n" + indent);
        }*/

        public override string StatementAsYangString(int indentationlevel)
        {
            throw new NotImplementedException();
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
