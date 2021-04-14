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
    public class InputStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/InputStatement/InputStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the input statement is parsed correctly.
        /// </summary>
        [Test]
        public void InputIsParsedCorrectly()
        {
            var input = InterpreterCorrect.Root.Descendants("input").Single();
            Assert.AreEqual(9, input.Elements().Count());
            Assert.AreEqual(null, input.Value);
        }

        /// <summary>
        /// Checks if the input statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void InputStatementWhitelistOverflowError()
        {
            var input = InterpreterCorrect.Root.Descendants("input").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => input.AddStatement(new BooleanTypeStatement()));
        }
    }
}