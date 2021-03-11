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
    public class ContainerStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ContainerStatement/ContainerStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the container statement is parsed correctly.
        /// </summary>
        [Test]
        public void ContainerStatementIsParsedCorrectly()
        {
            var container = InterpreterCorrect.Root.Descendants("container").Where(statement => statement.Value == "mainTester").Single();
            Assert.AreEqual(17, container.Elements().Count());
        }

        /// <summary>
        /// Checks if the container statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void ContainerStatementWhitelistOverflowError()
        {
            var container = InterpreterCorrect.Root.Descendants("container").Where(statement => statement.Value == "mainTester").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new PresenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new ReferenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new StatusStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.AddStatement(new WhenStatement()));
        }
    }
}