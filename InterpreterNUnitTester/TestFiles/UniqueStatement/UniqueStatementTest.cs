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
    public class UniqueStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/UniqueStatement/UniqueStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the unique statement is parsed correctly.
        /// </summary>
        [Test]
        public void UniqueStatementIsParsedCorrectly()
        {
            var unique = InterpreterCorrect.Root.Descendants("unique").Single();
            Assert.AreEqual("identifier identifier2", unique.Value);
        }

        /// <summary>
        /// Checks if the key statement is childless.
        /// </summary>
        [Test]
        public void uniqueStatementIsChildless()
        {
            var unique = InterpreterCorrect.Root.Descendants("unique").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => unique.AddStatement(new EmptyLineStatement()));
        }
    }
}