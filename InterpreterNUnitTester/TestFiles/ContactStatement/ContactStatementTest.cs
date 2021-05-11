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
    public class ContactStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ContactStatement/ContactStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the contact statement is parsed correctly.
        /// </summary>
        [Test]
        public void ContactIsParsedCorrectly()
        {
            var contact = InterpreterCorrect.Root.Descendants("contact").Single();
            Assert.AreEqual("Email: adam.sranko@gmail.com", contact.Argument);
        }

        /// <summary>
        /// Checks if the contact statement is childless.
        /// </summary>
        [Test]
        public void ContactIsChildless()
        {
            var contact = InterpreterCorrect.Root.Descendants("contact").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => contact.AddStatement(new EmptyLineStatement()));
        }
    }
}