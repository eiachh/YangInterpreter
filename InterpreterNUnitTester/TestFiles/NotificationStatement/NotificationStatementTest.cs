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
    public class NotificationStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/NotificationStatement/NotificationStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the notification statement is parsed correctly.
        /// </summary>
        [Test]
        public void NotificationIsParsedCorrectly()
        {
            var notification = InterpreterCorrect.Root.Descendants("notification").Single(statement => statement.Argument == "mainTester");
            Assert.AreEqual(13, notification.Elements().Count());
        }

        /// <summary>
        /// Checks if the notification statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void NotificationStatementWhitelistOverflowError()
        {
            var notification = InterpreterCorrect.Root.Descendants("notification").Single(statement => statement.Argument == "mainTester");
            Assert.Throws<ArgumentOutOfRangeException>(() => notification.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => notification.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => notification.AddStatement(new ReferenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => notification.AddStatement(new StatusStatement()));
        }
    }
}