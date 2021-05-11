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
    public class MaxElementsStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/MaxElementsStatement/MaxElementsStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the max-elements statement is parsed correctly.
        /// </summary>
        [Test]
        public void MaxElementsStatementIsParsedCorrectly()
        {
            var maxElem = InterpreterCorrect.Root.Descendants("max-elements").Single();
            Assert.AreEqual("+2", maxElem.Argument);
        }

        /// <summary>
        /// Checks if the max-elements statement default value is unbounded.
        /// </summary>
        [Test]
        public void MaxElementsStatementDefaultValue()
        {
            var maxElem = new MaxElementsStatement();
            Assert.AreEqual("unbounded", maxElem.Argument);
        }

        /// <summary>
        /// Checks if the max-elements statement is childless.
        /// </summary>
        [Test]
        public void MaxElementsStatementIsChildless()
        {
            var maxElem = InterpreterCorrect.Root.Descendants("max-elements").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => maxElem.AddStatement(new EmptyLineStatement()));
        }

        /// <summary>
        /// Checks if the max-elements statement`s value is restricted correctly.
        /// </summary>
        [Test]
        public void MaxElementsStatementValueRestriction()
        {
            Assert.Throws<ImproperValue>(() => new MaxElementsStatement("a"));
            Assert.Throws<ImproperValue>(() => new MaxElementsStatement("-2"));
            Assert.Throws<ImproperValue>(() => new MaxElementsStatement("+"));
            Assert.Throws<ImproperValue>(() => new MaxElementsStatement(""));
        }
    }
}