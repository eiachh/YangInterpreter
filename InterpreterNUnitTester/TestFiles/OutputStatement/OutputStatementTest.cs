using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class OutputStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/OutputStatement/OutputStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the output statement is parsed correctly.
        /// </summary>
        [Test]
        public void OutputIsParsedCorrectly()
        {
            var output = InterpreterCorrect.Root.Descendants("output").Single();
            Assert.AreEqual(9, output.Elements().Count());
            Assert.AreEqual(null, output.Argument);
        }

        /// <summary>
        /// Checks if the output statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void OutputStatementWhitelistOverflowError()
        {
            var output = InterpreterCorrect.Root.Descendants("output").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => output.AddStatement(new BooleanTypeStatement()));
        }
    }
}