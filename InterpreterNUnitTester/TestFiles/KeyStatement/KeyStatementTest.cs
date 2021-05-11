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
    public class KeyStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/KeyStatement/KeyStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the key statement is parsed correctly.
        /// </summary>
        [Test]
        public void KeyStatementIsParsedCorrectly()
        {
            var key = InterpreterCorrect.Root.Descendants("key").Single();
            Assert.AreEqual("identifier identifier2", key.Argument);
        }

        /// <summary>
        /// Checks if the key statement is childless.
        /// </summary>
        [Test]
        public void KeyStatementIsChildless()
        {
            var key = InterpreterCorrect.Root.Descendants("key").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => key.AddStatement(new EmptyLineStatement()));
        }
    }
}