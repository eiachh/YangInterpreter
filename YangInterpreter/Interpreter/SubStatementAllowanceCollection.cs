using System;
using System.Collections.Generic;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements.Types;

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

        internal static Dictionary<Type, Tuple<int, int>> AnyXmlStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> MustStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> AugmentStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> GroupingStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> LeafListStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> TypedefStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> UsesStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ListStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> DeviationStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> DeviateStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ArgumentStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ExtensionStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> FeatureStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> IncludeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> IdentityStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> NotificationStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> RpcStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> InputStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> OutputStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
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

                FillAnyXmlStatementAllowanceList();

                FillBitsSntatementAllowanceList();
                FillChoiceCaseSntatementAllowanceList();
                FillChoiceStatementAllowanceList();
                FillModuleSntatementAllowanceList();
                FillImportStatementAllowanceList();
                FillLeafStatementAllowanceList();
                FillRevisionStatementAllowanceList();
                FillEnumStatementAllowanceList();
                FillPatternStatementAllowanceList();
                FillLengthStatementAllowanceList();
                FillRangeStatementAllowanceList();
                FillMustStatementAllowanceList();
                FillAugmentsStatementAllowanceList();
                FillContainerStatementAllowanceList();
                FillGroupingStatementAllowanceList();
                FillLeafListStatementAllowanceList();
                FillTypedefStatementAllowanceList();
                FillUsesStatementAllowanceList();
                FillListStatementAllowanceList();
                FillDeviationStatementAllowanceList();
                FillDeviateStatementAllowanceList();
                FillArgumentStatementAllowanceList();
                FillExtensionStatementAllowanceList();
                FillFeatureStatementAllowanceList();
                FillIncludeStatementAllowanceList();
                FillIdentityStatementAllowanceList();
                FillNotificationStatementAllowanceList();
                FillRpcStatementAllowanceList();
                FillInputStatementAllowanceList();
                FillOutputStatementAllowanceList();
            }
        }

        private static void FillModuleSntatementAllowanceList()
        {
            ModuleStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(AugmentStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(ContactStatement), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(DeviationStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(ExtensionStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(FeatureStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(IdentityStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(ImportStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(IncludeStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(NamespaceStatement), new Tuple<int, int>(1, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(NotificationStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(OrganizationStatement), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(PrefixStatement), new Tuple<int, int>(1, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            ModuleStatementAllowedSubstatements.Add(typeof(RevisionStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(RpcStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            ModuleStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));

            //Handled differently maximum 1 is present (1 is always present and overwritten if new one is added)
            ModuleStatementAllowedSubstatements.Add(typeof(YangVersionStatement), new Tuple<int, int>(0, 2));
        }
        private static void FillBitsSntatementAllowanceList()
        {
            BitStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            BitStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            BitStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            BitStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            BitStatementAllowedSubstatements.Add(typeof(PositionStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillChoiceCaseSntatementAllowanceList()
        {
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
            ChoiceCaseStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }

        private static void FillChoiceStatementAllowanceList()
        {
            ChoiceStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(ChoiceCaseStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            ChoiceStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(DefaultStatement), new Tuple<int, int>(0, 1));
            ChoiceStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            ChoiceStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            ChoiceStatementAllowedSubstatements.Add(typeof(MandatoryStatement), new Tuple<int, int>(0, 1));
            ChoiceStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            ChoiceStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            ChoiceStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }

        private static void FillImportStatementAllowanceList()
        {
            ImportStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ImportStatementAllowedSubstatements.Add(typeof(PrefixStatement), new Tuple<int, int>(1, 1));
            ImportStatementAllowedSubstatements.Add(typeof(RevisionDateStatement), new Tuple<int, int>(0, 1));
        }

        private static void FillLeafStatementAllowanceList()
        {
            LeafStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(DefaultStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(MandatoryStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(MustStatement), new Tuple<int, int>(0, -1));
            LeafStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            #region Type Statements

            LeafStatementAllowedSubstatements.Add(typeof(BitsTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(EmptyTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(StringTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(LeafRefTypeStatement), new Tuple<int, int>(0, 1));

            LeafStatementAllowedSubstatements.Add(typeof(Int8TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(Int16TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(Int32TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(Int64TypeStatement), new Tuple<int, int>(0, 1));

            LeafStatementAllowedSubstatements.Add(typeof(UInt8TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(UInt16TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(UInt32TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(UInt64TypeStatement), new Tuple<int, int>(0, 1));

            LeafStatementAllowedSubstatements.Add(typeof(Decimal64TypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(InstanceIdentifierTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(BinaryTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(BooleanTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(IdentityrefTypeStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(UnionTypeStatement), new Tuple<int, int>(0, 1));
            #endregion
            LeafStatementAllowedSubstatements.Add(typeof(UnitsStatement), new Tuple<int, int>(0, 1));
            LeafStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }

        private static void FillRevisionStatementAllowanceList()
        {
            RevisionStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            RevisionStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            RevisionStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillEnumStatementAllowanceList()
        {
            EnumStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            EnumStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            EnumStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            EnumStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            EnumStatementAllowedSubstatements.Add(typeof(ValueStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillBinaryTypeStatementAllowanceList()
        {
            BinaryTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            BinaryTypeStatementAllowedSubstatements.Add(typeof(LengthStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillBitTypeStatementAllowanceList()
        {
            BitTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            BitTypeStatementAllowedSubstatements.Add(typeof(BitStatement), new Tuple<int, int>(0, -1));
        }

        private static void FillStringTypeStatementAllowanceList()
        {
            StringTypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            StringTypeStatementAllowedSubstatements.Add(typeof(LengthStatement), new Tuple<int, int>(0, 1));
            StringTypeStatementAllowedSubstatements.Add(typeof(PatternStatement), new Tuple<int, int>(0, -1));
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
            PatternStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            PatternStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            PatternStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            PatternStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillRangeStatementAllowanceList()
        {
            RangeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            RangeStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            RangeStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            RangeStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            RangeStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillLengthStatementAllowanceList()
        {
            LengthStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            LengthStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            LengthStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            LengthStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            LengthStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
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
        private static void FillAnyXmlStatementAllowanceList()
        {
            AnyXmlStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(MandatoryStatement), new Tuple<int, int>(0, 1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(MustStatement), new Tuple<int, int>(0, -1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            AnyXmlStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillMustStatementAllowanceList()
        {
            MustStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            MustStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            MustStatementAllowedSubstatements.Add(typeof(ErrorAppTagStatement), new Tuple<int, int>(0, 1));
            MustStatementAllowedSubstatements.Add(typeof(ErrorMessageStatement), new Tuple<int, int>(0, 1));
            MustStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillAugmentsStatementAllowanceList()
        {
            AugmentStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(ChoiceCaseStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            AugmentStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            AugmentStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            AugmentStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
            AugmentStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillContainerStatementAllowanceList()
        {
            ContainerStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            ContainerStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            ContainerStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, 1));
            ContainerStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(MustStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(PresenceStatement), new Tuple<int, int>(0, 1));
            ContainerStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            ContainerStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            ContainerStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
            ContainerStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillGroupingStatementAllowanceList()
        {
            GroupingStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            GroupingStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            GroupingStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            GroupingStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            GroupingStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
        }
        private static void FillLeafListStatementAllowanceList()
        {
            LeafListStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            LeafListStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            LeafListStatementAllowedSubstatements.Add(typeof(MaxElementsStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(MinElementsStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(MustStatement), new Tuple<int, int>(0, -1));
            LeafListStatementAllowedSubstatements.Add(typeof(OrderedByStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));

            #region Type Statements

            LeafListStatementAllowedSubstatements.Add(typeof(BitsTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(EmptyTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(StringTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(LeafRefTypeStatement), new Tuple<int, int>(0, 1));

            LeafListStatementAllowedSubstatements.Add(typeof(Int8TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(Int16TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(Int32TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(Int64TypeStatement), new Tuple<int, int>(0, 1));

            LeafListStatementAllowedSubstatements.Add(typeof(UInt8TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(UInt16TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(UInt32TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(UInt64TypeStatement), new Tuple<int, int>(0, 1));

            LeafListStatementAllowedSubstatements.Add(typeof(Decimal64TypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(InstanceIdentifierTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(BinaryTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(BooleanTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(IdentityrefTypeStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(UnionTypeStatement), new Tuple<int, int>(0, 1));
            #endregion

            LeafListStatementAllowedSubstatements.Add(typeof(UnitsStatement), new Tuple<int, int>(0, 1));
            LeafListStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillTypedefStatementAllowanceList()
        {
            TypedefStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            TypedefStatementAllowedSubstatements.Add(typeof(DefaultStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));

            #region Type Statements

            TypedefStatementAllowedSubstatements.Add(typeof(BitsTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(EmptyTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(StringTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(LeafRefTypeStatement), new Tuple<int, int>(0, 1));

            TypedefStatementAllowedSubstatements.Add(typeof(Int8TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(Int16TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(Int32TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(Int64TypeStatement), new Tuple<int, int>(0, 1));

            TypedefStatementAllowedSubstatements.Add(typeof(UInt8TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(UInt16TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(UInt32TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(UInt64TypeStatement), new Tuple<int, int>(0, 1));

            TypedefStatementAllowedSubstatements.Add(typeof(Decimal64TypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(InstanceIdentifierTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(BinaryTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(BooleanTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(IdentityrefTypeStatement), new Tuple<int, int>(0, 1));
            TypedefStatementAllowedSubstatements.Add(typeof(UnionTypeStatement), new Tuple<int, int>(0, 1));
            #endregion

            TypedefStatementAllowedSubstatements.Add(typeof(UnitsStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillUsesStatementAllowanceList()
        {
            UsesStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            UsesStatementAllowedSubstatements.Add(typeof(AugmentStatement), new Tuple<int, int>(0, 1));
            UsesStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            UsesStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            UsesStatementAllowedSubstatements.Add(typeof(RefineStatement), new Tuple<int, int>(0, 1));
            UsesStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            UsesStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            UsesStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillListStatementAllowanceList()
        {
            ListStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(KeyStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(MaxElementsStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(MinElementsStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(MustStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(OrderedByStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            ListStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(UniqueStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
            ListStatementAllowedSubstatements.Add(typeof(WhenStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillDeviationStatementAllowanceList()
        {
            DeviationStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            DeviationStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            DeviationStatementAllowedSubstatements.Add(typeof(DeviateStatement), new Tuple<int, int>(1, -1));
            DeviationStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillDeviateStatementAllowanceList()
        {
            DeviateStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            DeviateStatementAllowedSubstatements.Add(typeof(ConfigStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(DefaultStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(MandatoryStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(MaxElementsStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(MinElementsStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(MustStatement), new Tuple<int, int>(0, -1));

            #region Type Statements

            DeviateStatementAllowedSubstatements.Add(typeof(BitsTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(EmptyTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(StringTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(LeafRefTypeStatement), new Tuple<int, int>(0, 1));

            DeviateStatementAllowedSubstatements.Add(typeof(Int8TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(Int16TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(Int32TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(Int64TypeStatement), new Tuple<int, int>(0, 1));

            DeviateStatementAllowedSubstatements.Add(typeof(UInt8TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(UInt16TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(UInt32TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(UInt64TypeStatement), new Tuple<int, int>(0, 1));

            DeviateStatementAllowedSubstatements.Add(typeof(Decimal64TypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(InstanceIdentifierTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(BinaryTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(BooleanTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(IdentityrefTypeStatement), new Tuple<int, int>(0, 1));
            DeviateStatementAllowedSubstatements.Add(typeof(UnionTypeStatement), new Tuple<int, int>(0, 1));
            #endregion

            DeviateStatementAllowedSubstatements.Add(typeof(UniqueStatement), new Tuple<int, int>(0, -1));
            DeviateStatementAllowedSubstatements.Add(typeof(UnitsStatement), new Tuple<int, int>(0, 1));

        }
        private static void FillArgumentStatementAllowanceList()
        {
            ArgumentStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ArgumentStatementAllowedSubstatements.Add(typeof(YinElementStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillExtensionStatementAllowanceList()
        {
            ExtensionStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            ExtensionStatementAllowedSubstatements.Add(typeof(ArgumentStatement), new Tuple<int, int>(0, 1));
            ExtensionStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            ExtensionStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            ExtensionStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillFeatureStatementAllowanceList()
        {
            FeatureStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            FeatureStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            FeatureStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            FeatureStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            FeatureStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillIncludeStatementAllowanceList()
        {
            IncludeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            IncludeStatementAllowedSubstatements.Add(typeof(RevisionDateStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillIdentityStatementAllowanceList()
        {
            IdentityStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            IdentityStatementAllowedSubstatements.Add(typeof(BaseStatement), new Tuple<int, int>(0, 1));
            IdentityStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            IdentityStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            IdentityStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
        }
        private static void FillNotificationStatementAllowanceList()
        {
            NotificationStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            NotificationStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            NotificationStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            NotificationStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            NotificationStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
        }
        private static void FillRpcStatementAllowanceList()
        {
            RpcStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            RpcStatementAllowedSubstatements.Add(typeof(DescriptionStatement), new Tuple<int, int>(0, 1));
            RpcStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            RpcStatementAllowedSubstatements.Add(typeof(IfFeatureStatement), new Tuple<int, int>(0, -1));
            RpcStatementAllowedSubstatements.Add(typeof(InputStatement), new Tuple<int, int>(0, 1));
            RpcStatementAllowedSubstatements.Add(typeof(OutputStatement), new Tuple<int, int>(0, 1));
            RpcStatementAllowedSubstatements.Add(typeof(ReferenceStatement), new Tuple<int, int>(0, 1));
            RpcStatementAllowedSubstatements.Add(typeof(StatusStatement), new Tuple<int, int>(0, 1));
            RpcStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
        }
        private static void FillInputStatementAllowanceList()
        {
            InputStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            InputStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
        }
        private static void FillOutputStatementAllowanceList()
        {
            OutputStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(AnyXmlStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(ChoiceStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(ContainerStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(GroupingStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(LeafStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(LeafListStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(ListStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(TypedefStatement), new Tuple<int, int>(0, -1));
            OutputStatementAllowedSubstatements.Add(typeof(UsesStatement), new Tuple<int, int>(0, -1));
        }
    }
}
