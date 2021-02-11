using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;
using System.ComponentModel;
using System.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public enum BuiltInTypes
    {
        binary,
        bits,
        boolean,
        decimal64,
        empty,
        enumeration,
        identityref,
        instance_identifier,
        int8,
        int16,
        int32,
        int64,
        leafref,
        string_yang,
        uint8,
        uint16,
        uint32,
        uint64,
        union,
        none,
    }
    public abstract class TypeStatement : ContainerStatementBase
    {
        /// <summary>
        /// List of allowed substatements and the maximum allowed occurence of them.
        /// </summary>
        internal static List<Tuple<Type,int>> AllowedSubstatements = new List<Tuple<Type, int>>();
        public BuiltInTypes BuiltInTypeOfNode { get; set; } = BuiltInTypes.none;
        public TypeStatement(BuiltInTypes BaseType) : base(BuiltInTypeToString(BaseType))
        {
            BuiltInTypeOfNode = BaseType;
        }
        internal static string BuiltInTypeToString(BuiltInTypes type)
        {
            if(type == BuiltInTypes.string_yang)
            {
                return "string";
            }
            else if(type == BuiltInTypes.instance_identifier)
            {
                return "instance-identifier";
            }
            else
            {
                return type.ToString();
            }
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary() 
        { 
            return SubStatementAllowanceCollection.TypeStatementAllowedSubstatements; 
        }
    }
}
