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
    public class DerivedTypeStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/DerivedTypeStatement/DerivedTypeStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision statement is parsed correctly.
        /// </summary>
        [Test]
        public void IsParsedCorrectly()
        {
            var derType = InterpreterCorrect.Root.Descendants("type").Single();
            Assert.AreEqual("derivedTestType", derType.Argument);
        }
    }
}