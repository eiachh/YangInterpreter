using System;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Interpreter
{
    internal static class SubStatementAllowanceCollection
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                 Lists of allowed substatements and the maximum allowed occurence of them.
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static Dictionary<Type, Tuple<int, int>> TypeStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> BitStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ChoiceCaseStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ChoiceStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ContainerStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ImportStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> LeafStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> ModuleStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
        internal static Dictionary<Type, Tuple<int, int>> RevisionStatementAllowedSubstatements = new Dictionary<Type, Tuple<int, int>>();
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
                FillBitsSntatementAllowanceList();
                FillChoiceCaseSntatementAllowanceList();
                FillChoiceSntatementAllowanceList();
                FillTypeSntatementAllowanceList();
                FillModuleSntatementAllowanceList();
                FillImportSntatementAllowanceList();
                FillLeafSntatementAllowanceList();
                FillRevisionSntatementAllowanceList();
            }
        }

        private static void FillTypeSntatementAllowanceList()
        {
            TypeStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));

            TypeStatementAllowedSubstatements.Add(typeof(Bit), new Tuple<int, int>(0, -1));
            //TypeStatementAllowedSubstatements.Add(typeof(EnumTypeStatement), new Tuple<int, int>(0, -1));
            //TypeStatementAllowedSubstatements.Add(typeof(Length), new Tuple<int, int>(0, 1));
            //TypeStatementAllowedSubstatements.Add(typeof(Path), new Tuple<int, int>(0, 1));
            //TypeStatementAllowedSubstatements.Add(typeof(Pattern), new Tuple<int, int>(0, -1));
            //TypeStatementAllowedSubstatements.Add(typeof(Range), new Tuple<int, int>(0, -1));
            //TypeStatementAllowedSubstatements.Add(typeof(RequireInstance), new Tuple<int, int>(0, 1));
            TypeStatementAllowedSubstatements.Add(typeof(TypeStatement), new Tuple<int, int>(0, 1));

            TypeStatementAllowedSubstatements.Add(typeof(EmptyTypeStatement), new Tuple<int, int>(0, -1));
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
            //BitStatementAllowedSubstatements.Add(typeof(Status), new Tuple<int, int>(0, 1));
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
        }

        private static void FillRevisionSntatementAllowanceList()
        {
            RevisionStatementAllowedSubstatements.Add(typeof(EmptyLineStatement), new Tuple<int, int>(0, -1));
            RevisionStatementAllowedSubstatements.Add(typeof(Description), new Tuple<int, int>(0, 1));
            RevisionStatementAllowedSubstatements.Add(typeof(Reference), new Tuple<int, int>(0, 1));
        }
    }
}
