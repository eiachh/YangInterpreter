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
    public class ChoiceStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ChoiceStatement/ChoiceStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the choice statement is parsed correctly.
        /// </summary>
        [Test]
        public void ChoiceIsParsedCorrectly()
        {
            var choice = InterpreterCorrect.Root.Descendants("choice").Where(statement => statement.Value == "mainTester").Single();
            
            //Contains 2 case ==> 14 + 1
            Assert.AreEqual(15, choice.Elements().Count());
        }

        /// <summary>
        /// Checks if the choice statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void ChoiceStatementWhitelistOverflowError()
        {
            var choice = InterpreterCorrect.Root.Descendants("choice").Where(statement => statement.Value == "mainTester").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new DefaultStatement("case1")));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new MandatoryStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new ReferenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new StatusStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => choice.AddStatement(new WhenStatement()));
        }
    }
}