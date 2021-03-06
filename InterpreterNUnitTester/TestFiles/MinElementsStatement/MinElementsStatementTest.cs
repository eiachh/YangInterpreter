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
    public class MinElementsStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/MinElementsStatement/MinElementsStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the min-elements statement is parsed correctly.
        /// </summary>
        [Test]
        public void MinElementsStatementIsParsedCorrectly()
        {
            var minElem = InterpreterCorrect.Root.Descendants("min-elements").Single();
            Assert.AreEqual("+2", minElem.Value);
        }

        /// <summary>
        /// Checks if the min-elements statement default value is unbounded.
        /// </summary>
        [Test]
        public void MinElementsStatementDefaultValue()
        {
            var minElem = new MinElementsStatement();
            Assert.AreEqual("0", minElem.Value);
        }

        /// <summary>
        /// Checks if the min-elements statement is childless.
        /// </summary>
        [Test]
        public void MinElementsStatementIsChildless()
        {
            var minElem = InterpreterCorrect.Root.Descendants("min-elements").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => minElem.AddStatement(new EmptyLineStatement()));
        }

        /// <summary>
        /// Checks if the min-elements statement`s value is restricted correctly.
        /// </summary>
        [Test]
        public void MinElementsStatementValueRestriction()
        {
            Assert.Throws<ImproperValue>(() => new MinElementsStatement("a"));
            Assert.Throws<ImproperValue>(() => new MinElementsStatement("-2"));
            Assert.Throws<ImproperValue>(() => new MinElementsStatement("+"));
            Assert.Throws<ImproperValue>(() => new MinElementsStatement(""));
        }
    }
}