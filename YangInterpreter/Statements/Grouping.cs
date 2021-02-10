﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class Grouping : Statement
    {
        public static List<Grouping> GruopingList = new List<Grouping>();
        public static List<Uses> UsesWaitingForSpecifiedGrouping = new List<Uses>();

        public Grouping(string name) : base(name)
        {
            GruopingList.Add(this);
            foreach (var uses in UsesWaitingForSpecifiedGrouping)
            {
                if (uses.Name == name)
                {
                    uses.ContainedGrouping = this;
                    UsesWaitingForSpecifiedGrouping.Remove(uses);
                }
            }
        }

        public static Grouping GetGroupingByName(string groupingname, Uses caller)
        {
            foreach (var grouping in GruopingList)
            {
                if (grouping.Name == groupingname)
                {
                    return grouping;
                }
            }
            RegisterUsesForWaitingList(caller);
            return null;
        }
        private static void RegisterUsesForWaitingList(Uses uses)
        {
            UsesWaitingForSpecifiedGrouping.Add(uses);
        }

        public override string StatementAsYangString(int identationlevel)
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString()
        {
            throw new NotImplementedException();
        }

        public XElement[] NodeAsXmlForUses()
        {
            List<XElement> retchildlist = new List<XElement>();
            foreach (var child in StatementList)
            {
                retchildlist.AddRange(child.StatementAsXML());
            }
            return retchildlist.ToArray();
        }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }
    }
}