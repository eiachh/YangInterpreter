using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Property
{
    internal class EnumPropertyGroup : BaseStatement
    {
        public List<EnumStatement> EnumList = new List<EnumStatement>();

        public override XElement[] StatementAsXML()
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

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }

        /*internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            throw new NotImplementedException();
        }*/
    }
}
