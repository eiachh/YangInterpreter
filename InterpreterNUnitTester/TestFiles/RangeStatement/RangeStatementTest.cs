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
    public class RangeStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/RangeStatement/RangeStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the Range statement is parsed correctly.
        /// </summary>
        [Test]
        public void RangeIsParsedCorrectly()
        {
            var rangeStatement1 = InterpreterCorrect.Root.Descendants("range").First();
            Assert.AreEqual("2..10 \r\n| 51..343", rangeStatement1.Argument);
        }

        /// <summary>
        /// Checks if the error-app-tag is parsed correctly.
        /// </summary>
        [Test]
        public void RangeErrorAppTagIsParsedCorrectly()
        {
            var rangeStatements = InterpreterCorrect.Root.Descendants("range");
            var rangeStatement1 = rangeStatements.First();
            var errorAppStatement = rangeStatement1.Elements("error-app-tag").Single();
            Assert.AreEqual("the error app tag", errorAppStatement.Argument);

        }

        /// <summary>
        /// Checks if the error-message is parsed correctly.
        /// </summary>
        [Test]
        public void RangeErrorMessageIsParsedCorrectly()
        {
            var rangeStatement1 = InterpreterCorrect.Root.Descendants("range").First();
            var errormessageStatement = rangeStatement1.Elements("error-message").Single();
            Assert.AreEqual("the error message", errormessageStatement.Argument);
        }

        /// <summary>
        /// Checks if the reference is parsed correctly.
        /// </summary>
        [Test]
        public void RangeReferenceIsParsedCorrectly()
        {
            var rangeStatement1 = InterpreterCorrect.Root.Descendants("range").First();
            var errormessageStatement = rangeStatement1.Elements("reference").Single();
            Assert.AreEqual("the reference", errormessageStatement.Argument);
        }

        /// <summary>
        /// Throws error if error-app-tag is added more than 1 times.
        /// </summary>
        [Test]
        public void ErrorAppTagOverFlowCheck()
        {
            var rangeStatement1 = new RangeStatement("2..10 | 51..343");
            rangeStatement1.AddStatement(new ErrorAppTagStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => rangeStatement1.AddStatement(new ErrorAppTagStatement("value")));
        }

        /// <summary>
        /// Throws error if error-message is added more than 1 times.
        /// </summary>
        [Test]
        public void ErrorMessageOverFlowCheck()
        {
            var rangeStatement1 = new RangeStatement("2..10 | 51..343");
            rangeStatement1.AddStatement(new ErrorMessageStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => rangeStatement1.AddStatement(new ErrorMessageStatement("value")));
        }

        /// <summary>
        /// Throws error if reference is added more than 1 times.
        /// </summary>
        [Test]
        public void ReferenceOverFlowCheck()
        {
            var rangeStatement1 = new RangeStatement("2..10 | 51..343");
            rangeStatement1.AddStatement(new ReferenceStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => rangeStatement1.AddStatement(new ReferenceStatement("value")));
        }
    }
}