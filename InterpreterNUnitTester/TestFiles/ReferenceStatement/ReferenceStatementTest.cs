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
    public class ReferenceStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ReferenceStatement/ReferenceStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the reference statement is parsed correctly.
        /// </summary>
        [Test]
        public void ReferenceStatementIsParsedCorrectly()
        {
            var reference = InterpreterCorrect.Root.Descendants("reference").Single();
            Assert.AreEqual("Reference1102-2323", reference.Value);
        }
    }
}