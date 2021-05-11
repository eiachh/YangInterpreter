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
    public class ChoiceCaseStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ChoiceCaseStatement/ChoiceCaseStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the choice's case statement is parsed correctly.
        /// </summary>
        [Test]
        public void ChoiceCaseStatementIsParsedCorrectly()
        {
            var choicesCase = InterpreterCorrect.Root.Descendants("case").Where(statement => statement.Argument == "testCase").Single();
            Assert.AreEqual(12, choicesCase.Elements().Count());
        }

        /// <summary>
        /// Checks if the choice's case statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void ChoiceCaseStatementWhitelistOverflowError()
        {
            var choicesCase = InterpreterCorrect.Root.Descendants("case").Where(statement => statement.Argument == "testCase").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => choicesCase.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => choicesCase.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => choicesCase.AddStatement(new ReferenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => choicesCase.AddStatement(new StatusStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => choicesCase.AddStatement(new WhenStatement()));
        }
    }
}