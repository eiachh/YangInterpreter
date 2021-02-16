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

    /// Type Statement RFC 6020 7.4. 
    ///
    /// <summary>
    /// The "type" statement takes as an argument a string that is the name
    /// of a YANG built-in type(see Section 9) or a derived type(see
    /// Section 7.3), followed by an optional block of substatements that are
    /// used to put further restrictions on the type.
    /// </summary>
    /// 
    /// +------------------+---------+-------------+
    /// | substatement     | section | cardinality |
    /// +------------------+---------+-------------+
    /// | bit              | 9.7.4   | 0..n        |
    /// | enum             | 9.6.4   | 0..n        |
    /// | length           | 9.4.4   | 0..1        |
    /// | path             | 9.9.2   | 0..1        |
    /// | pattern          | 9.4.6   | 0..n        |
    /// | range            | 9.2.4   | 0..1        |
    /// | require-instance | 9.13.2  | 0..1        |
    /// | type             | 7.4     | 0..n        |
    /// +------------------+---------+-------------+

    public abstract class TypeStatement : ContainerStatementBase
    {
        /// <summary>
        /// List of allowed substatements and the maximum allowed occurence of them.
        /// </summary>
        internal static List<Tuple<Type,int>> AllowedSubstatements = new List<Tuple<Type, int>>();
        public BuiltInTypes BuiltInTypeOfNode { get; set; } = BuiltInTypes.none;
        public TypeStatement(BuiltInTypes BaseType) : base("type",BuiltInTypeToString(BaseType))
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

        /*internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary() 
        { 
            return SubStatementAllowanceCollection.TypeStatementAllowedSubstatements; 
        }*/
    }
}
