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
    public class ArgumentStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ArgumentStatement/ArgumentStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the argument statement is parsed correctly.
        /// </summary>
        [Test]
        public void ArgumentStatementIsParsedCorrectly()
        {
            var arg = InterpreterCorrect.Root.Descendants("argument").Single();
            Assert.AreEqual(1, arg.Elements().Count());
        }

        /// <summary>
        /// Checks if the argument statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void ArgumentStatementWhitelistOverflowError()
        {
            var arg = InterpreterCorrect.Root.Descendants("argument").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new YinElementStatement("true")));
        }
    }
}