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

        }

        /// <summary>
        /// Checks if the length statement is parsed correctly.
        /// </summary>
        [Test]
        public void LengthValueParsedCorrectly()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Length/TypeStatementStringTypeCorrect.yang");
            var length = InterpreterCorrect.Root.Descendants("length").Single();
            Assert.AreEqual("2323..5690", length.Value);
            Assert.AreEqual("length \"2323..5690\";", length.ToString());
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
            Assert.Throws<ImproperValue>(() => new Length("completly bad value"));
            Assert.Throws<ImproperValue>(() => new Length("23.445"));
        }
    }
}