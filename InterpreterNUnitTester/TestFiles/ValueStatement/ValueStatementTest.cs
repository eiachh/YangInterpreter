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
    public class ValueStatementTester
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ValueStatement/ValueStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the Value statements value is parsed correctly.
        /// </summary>
        [Test]
        public void ValueParsedCorrectly()
        {
            var valueStatement = InterpreterCorrect.Root.Descendants("value").First();
            Assert.AreEqual("7", valueStatement.Value);
        }

        /// <summary>
        /// Throws error if the value is not an integer when changed.
        /// </summary>
        [Test]
        public void ValueThrowsImproperValueAtChange()
        {
            var valueStatement = InterpreterCorrect.Root.Descendants("value").First();
            Assert.Throws<ImproperValue>(() => valueStatement.Value = "asd");
        }

        /// <summary>
        /// Throws error if the value is not an integer in constructor.
        /// </summary>
        [Test]
        public void ValueThrowsImproperValueAtConstructor()
        {
            Assert.Throws<ImproperValue>(() => new ValueStatement("error"));
        }
    }
}