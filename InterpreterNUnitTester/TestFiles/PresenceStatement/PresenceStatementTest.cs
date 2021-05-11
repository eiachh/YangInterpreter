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
    public class PresenceStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/PresenceStatement/PresenceStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the presence statement is parsed correctly.
        /// </summary>
        [Test]
        public void PresenceIsParsedCorrectly()
        {
            var presence = InterpreterCorrect.Root.Descendants("presence").Single();
            Assert.AreEqual("desc of presence meaning", presence.Argument);
        }

        /// <summary>
        /// Checks if the presence statement is childless.
        /// </summary>
        [Test]
        public void PresenceIsChildless()
        {
            var presence = InterpreterCorrect.Root.Descendants("presence").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => presence.AddStatement(new EmptyLineStatement()));
        }
    }
}