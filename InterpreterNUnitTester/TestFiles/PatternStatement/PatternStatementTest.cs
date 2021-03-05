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
    public class PatternStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/PatternStatement/PatternStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the pattern statement is parsed correctly.
        /// </summary>
        [Test]
        public void PatternIsParsedCorrectly()
        {
            var patternStatement = InterpreterCorrect.Root.Descendants("pattern").Single();
            Assert.AreEqual("[0-9a-fA-F]*", patternStatement.Value);
        }

        /// <summary>
        /// Checks if the error-app-tag is parsed correctly.
        /// </summary>
        [Test]
        public void PatternErrorAppTagIsParsedCorrectly()
        {
            var patternStatement = InterpreterCorrect.Root.Descendants("pattern").Single();
            var errorAppStatement = patternStatement.Elements("error-app-tag").Single();
            Assert.AreEqual("the error app tag", errorAppStatement.Value);

        }

        /// <summary>
        /// Checks if the error-message is parsed correctly.
        /// </summary>
        [Test]
        public void PatternErrorMessageIsParsedCorrectly()
        {
            var patternStatement = InterpreterCorrect.Root.Descendants("pattern").Single();
            var errormessageStatement = patternStatement.Elements("error-message").Single();
            Assert.AreEqual("the error message", errormessageStatement.Value);
        }

        /// <summary>
        /// Checks if the reference is parsed correctly.
        /// </summary>
        [Test]
        public void PatternReferenceIsParsedCorrectly()
        {
            var patternStatement = InterpreterCorrect.Root.Descendants("pattern").Single();
            var errormessageStatement = patternStatement.Elements("reference").Single();
            Assert.AreEqual("the reference", errormessageStatement.Value);
        }

        /// <summary>
        /// Throws error if error-app-tag is added more than 1 times.
        /// </summary>
        [Test]
        public void ErrorAppTagOverFlowCheck()
        {
            var pattern = new Pattern("pattern");
            pattern.AddStatement(new ErrorAppTagStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => pattern.AddStatement(new ErrorAppTagStatement("value")));
        }

        /// <summary>
        /// Throws error if error-message is added more than 1 times.
        /// </summary>
        [Test]
        public void ErrorMessageOverFlowCheck()
        {
            var pattern = new Pattern("pattern");
            pattern.AddStatement(new ErrorMessageStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => pattern.AddStatement(new ErrorMessageStatement("value")));
        }

        /// <summary>
        /// Throws error if reference is added more than 1 times.
        /// </summary>
        [Test]
        public void ReferenceOverFlowCheck()
        {
            var pattern = new Pattern("pattern");
            pattern.AddStatement(new ReferenceStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => pattern.AddStatement(new ReferenceStatement("value")));
        }
    }
}