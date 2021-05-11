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
    public class BooleanTypeStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Boolean/BooleanCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionIsParsedCorrectly()
        {
            var leafWithBooleanType = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "booleanTest").Single();
            Assert.AreEqual(1, leafWithBooleanType.Elements().Count());
            Assert.AreEqual("type boolean;",leafWithBooleanType.Elements().First().ToString());
        }
    }
}