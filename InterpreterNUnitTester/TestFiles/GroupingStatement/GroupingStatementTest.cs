using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements.Types;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;

namespace InterpreterNUnitTester
{
    public class GroupingStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/GroupingStatement/GroupingStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the grouping statement is parsed correctly.
        /// </summary>
        [Test]
        public void GroupingIsParsedCorrectly()
        {
            var grouping = InterpreterCorrect.Root.Descendants("grouping").Where(statement => statement.Argument == "mainTester").Single();
            Assert.AreEqual(12, grouping.Elements().Count());
        }

        /// <summary>
        /// Checks if the grouping statement is throwing error at whitelisted item overflow.
        /// </summary>
        [Test]
        public void GroupingWhitelistOverflowError()
        {
            var grouping = InterpreterCorrect.Root.Descendants("grouping").Where(statement => statement.Argument == "mainTester").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => grouping.AddStatement(new BooleanTypeStatement("true")));

            Assert.Throws<ArgumentOutOfRangeException>(() => grouping.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => grouping.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => grouping.AddStatement(new StatusStatement("current")));
        }
    }
}