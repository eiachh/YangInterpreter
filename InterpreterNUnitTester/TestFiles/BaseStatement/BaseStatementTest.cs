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
    public class BaseStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/BaseStatement/BaseStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionIsParsedCorrectly()
        {
            var identityType = InterpreterCorrect.Root.Descendants("type").Where(stat => stat.Value == "identityref").Single();
            Assert.AreEqual(1, identityType.Elements().Count());
            var baseStatement = identityType.Elements().First();
            Assert.AreEqual("nameSpace:SomeReference", baseStatement.Value);
        }
    }
}