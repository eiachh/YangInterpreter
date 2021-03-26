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
    public class AugmentStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/AugmentStatement/AugmentStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision statement is parsed correctly.
        /// </summary>
        [Test]
        public void AugmentIsParsedCorrectly()
        {
            var augment = InterpreterCorrect.Root.Descendants("augment").Where(statement => statement.Value == "mainTester").Single();
            Assert.AreEqual(13, augment.Elements().Count());
        }

        /// <summary>
        /// Checks if the augment statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void AugmentStatementWhitelistOverflowError()
        {
            var augment = InterpreterCorrect.Root.Descendants("augment").Where(statement => statement.Value == "mainTester").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => augment.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => augment.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => augment.AddStatement(new ReferenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => augment.AddStatement(new StatusStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => augment.AddStatement(new WhenStatement()));
        }
    }
}