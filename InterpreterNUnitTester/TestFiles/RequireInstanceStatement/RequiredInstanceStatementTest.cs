using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;

namespace InterpreterNUnitTester
{
    public class RequiredInstanceStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/RequireInstanceStatement/RequireInstanceStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the required-instance statement is parsed correctly.
        /// </summary>
        [Test]
        public void RequiredInstanceIsParsedCorrectly()
        {
            var instanceIdentifierLeaf = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "requireInstanceTest");
            Assert.AreEqual(1, instanceIdentifierLeaf.Count());

            var instanceIdentifier = instanceIdentifierLeaf.First().Elements();
            Assert.AreEqual(1, instanceIdentifier.Count());

            var requiredInstance = instanceIdentifier.First().Elements().FirstOrDefault();
            Assert.AreEqual("true", requiredInstance.Argument);
        }

        /// <summary>
        /// Checks if the required-instance statement is throwing error when wrong value is given.
        /// </summary>
        [Test]
        public void RequiredInstanceInvalidValueExceptionCheck()
        {
            Assert.Throws<ImproperValue>(() => new RequireInstanceStatement("notFalse"));
            Assert.Throws<ImproperValue>(() => new RequireInstanceStatement("2332332"));
            var RequiredInstanceCorrect = new RequireInstanceStatement("false");
            Assert.Throws<ImproperValue>(() => RequiredInstanceCorrect.Argument = "NotCorrectValue");
        }
    }
}