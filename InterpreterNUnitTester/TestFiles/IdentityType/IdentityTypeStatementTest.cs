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
    public class IdentityTypeStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/IdentityType/IdentityTypeStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the Identityref is parsed correctly.
        /// </summary>
        [Test]
        public void IdentityrefParsedCorrectly()
        {
            var leafOfIdentityref = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "identityTest").Single();
            Assert.AreEqual(1, leafOfIdentityref.Elements().Count());
        }
    }
}