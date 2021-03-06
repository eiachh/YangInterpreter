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
    public class RefineStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/RefineStatement/RefineStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the refine statement is parsed correctly.
        /// </summary>
        [Test]
        public void RefineIsParsedCorrectly()
        {
            var refine = InterpreterCorrect.Root.Descendants("refine").Single();
            Assert.AreEqual("identifier:ofThis", refine.Value);
        }

        /// <summary>
        /// Checks if the refine statement ischildless.
        /// </summary>
        [Test]
        public void RefineIsChildless()
        {
            var refine = InterpreterCorrect.Root.Descendants("refine").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => refine.AddStatement(new EmptyLineStatement()));
        }
    }
}