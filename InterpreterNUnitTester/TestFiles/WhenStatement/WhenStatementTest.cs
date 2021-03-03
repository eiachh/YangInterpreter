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
    public class WhenStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/WhenStatement/WhenStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the when statement is parsed correctly.
        /// </summary>
        [Test]
        public void WhenStatementIsParsedCorrectly()
        {
            var when = InterpreterCorrect.Root.Descendants("when").First();
            Assert.AreEqual("xpath:xml:something/", when.Value);
        }

        /// <summary>
        /// Checks if when statement cannot have child statements.
        /// </summary>
        [Test]
        public void WhenStatementIsChildless()
        {
            var when = InterpreterCorrect.Root.Descendants("when").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => when.AddStatement(new EmptyLineStatement()));
        }
    }
}