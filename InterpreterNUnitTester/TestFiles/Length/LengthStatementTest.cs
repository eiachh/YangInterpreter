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
    public class LengthStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Length/TypeStatementStringTypeCorrect.yang");
        }

        /// <summary>
        /// Checks if the length statement is parsed correctly.
        /// </summary>
        [Test]
        public void LengthValueParsedCorrectly()
        {
            var lengthToString = "length \"2323..5690\"; {\r\n\tdescription\r\n\t\t\"description of \r\n\t\tpattern.\";\r\n\terror-app-tag \"the error app tag\";\r\n\terror-message \"the error message\";\r\n\treference \"the reference\";\r\n}";
            var length = InterpreterCorrect.Root.Descendants("length").Single();
            Assert.AreEqual("2323..5690", length.Argument);
            Assert.AreEqual(lengthToString, length.ToString());
        }

        /// <summary>
        /// Throws error if the length statement is not formatted correctly.
        /// </summary>
        [Test]
        public void ThrowsErrorAtMalformedLengthStatement()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Length/TypeStatementStringTypeMalformed.yang"));
        }

        /// <summary>
        /// Throws error if the value of length is not correct;
        /// </summary>
        [Test]
        public void ThrowsErrorAtWrongValueInLength()
        {
            Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Length/TypeStatementStringTypeIncorrectValue.yang"));
            Assert.Throws<ImproperValue>(() => new LengthStatement("completly bad value"));
            Assert.Throws<ImproperValue>(() => new LengthStatement("23.445"));
        }

        /// <summary>
        /// Checks if the error-app-tag is parsed correctly.
        /// </summary>
        [Test]
        public void RangeErrorAppTagIsParsedCorrectly()
        {
            var lengthStatement1 = InterpreterCorrect.Root.Descendants("length").First();
            var errorAppStatement = lengthStatement1.Elements("error-app-tag").Single();
            Assert.AreEqual("the error app tag", errorAppStatement.Argument);

        }

        /// <summary>
        /// Checks if the error-message is parsed correctly.
        /// </summary>
        [Test]
        public void RangeErrorMessageIsParsedCorrectly()
        {
            var lengthStatement1 = InterpreterCorrect.Root.Descendants("length").First();
            var errormessageStatement = lengthStatement1.Elements("error-message").Single();
            Assert.AreEqual("the error message", errormessageStatement.Argument);
        }

        /// <summary>
        /// Checks if the reference is parsed correctly.
        /// </summary>
        [Test]
        public void RangeReferenceIsParsedCorrectly()
        {
            var lengthStatement1 = InterpreterCorrect.Root.Descendants("length").First();
            var errormessageStatement = lengthStatement1.Elements("reference").Single();
            Assert.AreEqual("the reference", errormessageStatement.Argument);
        }

        /// <summary>
        /// Throws error if error-app-tag is added more than 1 times.
        /// </summary>
        [Test]
        public void ErrorAppTagOverFlowCheck()
        {
            var lengthStatement1 = new LengthStatement("2323..5690");
            lengthStatement1.AddStatement(new ErrorAppTagStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => lengthStatement1.AddStatement(new ErrorAppTagStatement("value")));
        }

        /// <summary>
        /// Throws error if error-message is added more than 1 times.
        /// </summary>
        [Test]
        public void ErrorMessageOverFlowCheck()
        {
            var lengthStatement1 = new RangeStatement("2323..5690");
            lengthStatement1.AddStatement(new ErrorMessageStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => lengthStatement1.AddStatement(new ErrorMessageStatement("value")));
        }

        /// <summary>
        /// Throws error if reference is added more than 1 times.
        /// </summary>
        [Test]
        public void ReferenceOverFlowCheck()
        {
            var lengthStatement1 = new RangeStatement("2323..5690");
            lengthStatement1.AddStatement(new ReferenceStatement("value"));
            Assert.Throws<ArgumentOutOfRangeException>(() => lengthStatement1.AddStatement(new ReferenceStatement("value")));
        }
    }
}