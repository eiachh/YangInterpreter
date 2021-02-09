using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;
using System.ComponentModel;

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
    public abstract class TypeNode : Statement
    {
        public BuiltInTypes BuiltInTypeOfNode { get; set; } = BuiltInTypes.none;
        public TypeNode(BuiltInTypes BaseType) : base(BuiltInTypeToString(BaseType))
        {
            BuiltInTypeOfNode = BaseType;
            /*if(!YangTypes.IsValidType(name))
            {
                throw new ArgumentException("The given type for TypeNode is not a valid type: "+name);
            }*/
        }

        /*public override void AddChild(YangNode Node)
        {
            //Types can strictly contain other types only as children. 
            /*if (Node.GetType() == typeof(TypeNode) || Node.GetType() == typeof(YangEnum))
            {
                throw new TypeMissmatch("TypeNode can strictly contain other (TypeNodes or Enums) only as children.");
            }
            base.AddChild(Node);
        }*/

        /*public override string NodeAsYangString()
        {
            if (Range == null)
            {
                return string.Format("type {0};",Name);
            }
            return "type {0} {\r\n" + Range.PropertyAsYangText(1) + "\r\n}";
        }*/
        /*public override string NodeAsYangString(int indentationlevel)
        {
            if (Range == null)
            {
                return string.Format(GetIdentation(indentationlevel) + "type {0};", Name);
            }
            return GetIdentation(indentationlevel) + "type {0} {\r\n" +
                   GetIdentation(indentationlevel) + Range.PropertyAsYangText(indentationlevel+1) + "\r\n" +
                   GetIdentation(indentationlevel) + "}";
        }*/

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
    }
}
