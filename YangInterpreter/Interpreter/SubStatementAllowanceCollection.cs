using System;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Interpreter
{
    /// <summary>
    /// This static class is purely meant to store all the allowance list for statements. The allowance lists define what 
    /// substatement can a statement have and how many.
    /// </summary>
    internal static class SubStatementAllowanceCollection
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                 Lists of allowed substatements and the (minimum,maximum) allowed occurence of them.
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static Dictionary<Type, Tuple<int, int>> BinaryTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> BitTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> StringTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> EnumTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> IdentityrefTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> LeafRefTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> InstanceIdentifierTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> UnionTypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();

        internal static Dictionary<Type, Tuple<int, int>> Int64TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> Int32TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> Int16TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> Int8TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();

        internal static Dictionary<Type, Tuple<int, int>> UInt64TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> UInt32TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> UInt16TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> UInt8TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();

        internal static Dictionary<Type, Tuple<int, int>> Decimal64TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();

        internal static Dictionary<Type, Tuple<int, int>> BitStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ChoiceCaseStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ChoiceStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ContainerStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ImportStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> LeafStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ModuleStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> RevisionStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> EnumStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> PatternStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> LengthStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> RangeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        internal static bool IsInitialized = false;
        public static void Init()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                FillBinaryTypeStatementAllowanceList();
                FillBitTypeStatementAllowanceList();
                FillStringTypeStatementAllowanceList();
                FillEnumTypeStatementAllowanceList();
                FillLeafRefTypeStatementAllowanceList();
                FillInstanceIdentifierTypeStatementAllowanceList();
                FillIdentityrefTypeStatementAllowanceList();
                FillUnionTypeStatementAllowanceList();

                #region (U)Int types
                FillInt64TypeStatementAllowanceList();
                FillInt32TypeStatementAllowanceList();
                FillInt16TypeStatementAllowanceList();
                FillInt8TypeStatementAllowanceList();

                FillUInt64TypeStatementAllowanceList();
                FillUInt32TypeStatementAllowanceList();
                FillUInt16TypeStatementAllowanceList();
                FillUInt8TypeStatementAllowanceList();
                #endregion
                
                FillDecimal64TypeStatementAllowanceList();

                FillBitsSntatementAllowanceList();
                FillChoiceCaseSntatementAllowanceList();
                FillChoiceSntatementAllowanceList();
                FillModuleSntatementAllowanceList();
                FillImportSntatementAllowanceList();
                FillLeafSntatementAllowanceList();
                FillRevisionSntatementAllowanceList();
                FillEnumSntatementAllowanceList();
                FillPatternStatementAllowanceList();
                FillLengthStatementAllowanceList();
                FillRangeStatementAllowanceList();
            }
        }

        private static void FillModuleSntatementAllowanceList()
        {
            ModuleStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            //ModuleStatementAllowedSubstatements.Add(typeof(AnyXml), -1);
            //ModuleStatementAllowedSubstatements.Add(typeof(Augment), -1);
            ModuleStatementAllowedSubstatements.Add(typeof(Choices), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(Contact), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(Container), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            //ModuleStatementAllowedSubstatements.Add(typeof(Deviation), -1);
            //ModuleStatementAllowedSubstatements.Add(typeof(Extension), -1);
            //ModuleStatementAllowedSubstatements.Add(typeof(Feature), -1);
            ModuleStatementAllowedSubstatements.Add(typeof(Grouping), new Tuple<int, int>(0, -1));
            //ModuleStatementAllowedSubstatements.Add(typeof(Identity), -1);
            ModuleStatementAllowedSubstatements.Add(typeof(Import), new Tuple<int, int>(0, -1));
            //ModuleStatementAllowedSubstatements.Add(typeof(Include), -1);
            ModuleStatementAllowedSubstatements.Add(typeof(Leaf), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(LeafList), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(ListNode), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(NamespaceStatement), new Tuple<int, int>(1, 1));
            //ModuleStatementAllowedSubstatements.Add(typeof(Notification), -1);
            ModuleStatementAllowedSubstatements.Add(typeof(Organization), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(Prefix), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(Revision), new Tuple<int, int>(0, -1));
            //ModuleStatementAllowedSubstatements.Add(typeof(Rpc), new Tuple<int, int>(0, -1));
            //ModuleStatementAllowedSubstatements.Add(typeof(TypeDef), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(Uses), new Tuple<int, int>(0, -1));

            //Handled differently maximum 1 is present
            ModuleStatementAllowedSubstatements.Add(typeof(YangVersionStatement), new Tuple<int, int>(0, 2));
        }
        private static void FillBitsSntatementAllowanceList()
        {
            BitStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            BitStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            BitStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
            BitStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            BitStatementAllowedSubstatements.Add(typeof(Position), new Tuple<int, int>(0, 1));
        }
        private static void FillChoiceCaseSntatementAllowanceList()
        {
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
        }

        private static void FillChoiceSntatementAllowanceList()
        {
            ChoiceStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
        }

        private static void FillImportSntatementAllowanceList()
        {
            ImportStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ImportStatementAllowedSubstatements.Add(typeof(Prefix), new Tuple<int, int>(1, 1));
            //ImportStatementAllowedSubstatements.Add(typeof(RevisionDate), new Tuple<int, int>(0, 1));
        }

        private static void FillLeafSntatementAllowanceList()
        {
            LeafStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(BitsTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(EmptyTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(StringTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(LeafRefTypeStatement), new Tuple<int, int>(0, -1));

            LeafStatementAllowedSubstatements.Add(typeof(Int8TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(Int16TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(Int32TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(Int64TypeStatement), new Tuple<int, int>(0, -1));

            LeafStatementAllowedSubstatements.Add(typeof(UInt8TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(UInt16TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(UInt32TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(UInt64TypeStatement), new Tuple<int, int>(0, -1));

            LeafStatementAllowedSubstatements.Add(typeof(Decimal64TypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(InstanceIdentifierTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(BinaryTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(BooleanTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(IdentityrefTypeStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(UnionTypeStatement), new Tuple<int, int>(0, -1));
        }

        private static void FillRevisionSntatementAllowanceList()
        {
            RevisionStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            RevisionStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            RevisionStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
        }
        private static void FillEnumSntatementAllowanceList()
        {
            EnumStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            EnumStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            EnumStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
            EnumStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            EnumStatementAllowedSubstatements.Add(typeof(ValueStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillBinaryTypeStatementAllowanceList()
        {
            BinaryTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            BinaryTypeStatementAllowedSubstatements.Add(typeof(Length), new Tuple<int, int>(0, 1));
        }
        private static void FillBitTypeStatementAllowanceList()
        {
            BitTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            BitTypeStatementAllowedSubstatements.Add(typeof(Bit), new Tuple<int, int>(0, -1));
        }

        private static void FillStringTypeStatementAllowanceList()
        {
            StringTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            StringTypeStatementAllowedSubstatements.Add(typeof(Length), new Tuple<int, int>(0, 1));
            StringTypeStatementAllowedSubstatements.Add(typeof(Pattern), new Tuple<int, int>(0, -1));
        }

        private static void FillEnumTypeStatementAllowanceList()
        {
            EnumTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            EnumTypeStatementAllowedSubstatements.Add(typeof(EnumStatement), new Tuple<int, int>(0, -1));
        }
        private static void FillIdentityrefTypeStatementAllowanceList()
        {
            IdentityrefTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            IdentityrefTypeStatementAllowedSubstatements.Add(typeof(BaseStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillLeafRefTypeStatementAllowanceList()
        {
            LeafRefTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            LeafRefTypeStatementAllowedSubstatements.Add(typeof(PathStatement), new Tuple<int, int>(1, 1));
        }

        #region (U)Int types
        private static void FillInt64TypeStatementAllowanceList()
        {
            Int64TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            Int64TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillInt32TypeStatementAllowanceList()
        {
            Int32TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            Int32TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillInt16TypeStatementAllowanceList()
        {
            Int16TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            Int16TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillInt8TypeStatementAllowanceList()
        {
            Int8TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            Int8TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }

        private static void FillUInt64TypeStatementAllowanceList()
        {
            UInt64TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            UInt64TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillUInt32TypeStatementAllowanceList()
        {
            UInt32TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            UInt32TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillUInt16TypeStatementAllowanceList()
        {
            UInt16TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            UInt16TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillUInt8TypeStatementAllowanceList()
        {
            UInt8TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            UInt8TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }
        #endregion

        private static void FillDecimal64TypeStatementAllowanceList()
        {
            Decimal64TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            Decimal64TypeStatementAllowedSubstatements.Add(typeof(RangeStatement), new Tuple<int, int>(0, 1));
        }

        private static void FillPatternStatementAllowanceList()
        {
            PatternStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            PatternStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            PatternStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            PatternStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            PatternStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
        }
        private static void FillRangeStatementAllowanceList()
        {
            RangeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            RangeStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            RangeStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            RangeStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            RangeStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
        }
        private static void FillLengthStatementAllowanceList()
        {
            LengthStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            LengthStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            LengthStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            LengthStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            LengthStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
        }
        private static void FillInstanceIdentifierTypeStatementAllowanceList()
        {
            InstanceIdentifierTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            InstanceIdentifierTypeStatementAllowedSubstatements.Add(typeof(RequireInstanceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillUnionTypeStatementAllowanceList()
        {
            UnionTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(BinaryTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(BitsTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(BooleanTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(Decimal64TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(IdentityrefTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(InstanceIdentifierTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(Int16TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(Int32TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(Int64TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(Int8TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(StringTypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(UInt16TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(UInt32TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(UInt64TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(UInt8TypeStatement), new Tuple<int, int>(0, -1));
            UnionTypeStatementAllowedSubstatements.Add(typeof(UnionTypeStatement), new Tuple<int, int>(0, -1));
        }
    }
}
