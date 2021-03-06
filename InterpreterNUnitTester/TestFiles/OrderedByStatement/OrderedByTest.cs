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
    public class OrderedByTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/OrderedByStatement/OrderedByCorrect.yang");
        }

        /// <summary>
        /// Checks if the ordered-by statement is parsed correctly.
        /// </summary>
        [Test]
        public void OrderedByStatementIsParsedCorrectly()
        {
            var ord = InterpreterCorrect.Root.Descendants("ordered-by").Single();
            Assert.AreEqual("user", ord.Value);
        }

        /// <summary>
        /// Checks if the ordered-by statement value restriction is correct
        /// </summary>
        [Test]
        public void OrderedByStatementValueRestrictionCorrect()
        {
            Assert.Throws<ImproperValue>(() => new OrderedByStatement("notAllowed"));
            var ord = new OrderedByStatement();
            Assert.Throws<ImproperValue>(() => ord.Value = "invalid");
        }

        /// <summary>
        /// Checks if the ordered-by statement default value is correct.
        /// </summary>
        [Test]
        public void OrderedByStatementDefaultValue()
        {
            var ord = new OrderedByStatement();
            Assert.AreEqual("system", ord.Value);
        }

        /// <summary>
        /// Checks if the ordered-by statement is childless.
        /// </summary>
        [Test]
        public void OrderedByStatementIsChildless()
        {
            var ord = new OrderedByStatement();
            Assert.Throws<ArgumentOutOfRangeException>(() => ord.AddStatement(new EmptyLineStatement()));
        }
    }
}