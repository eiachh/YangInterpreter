using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class DeviationStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/DeviationStatement/DeviationStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the deviation statement is parsed correctly.
        /// </summary>
        [Test]
        public void DeviationIsParsedCorrectly()
        {
            var deviation = InterpreterCorrect.Root.Descendants("deviation").Single();
            Assert.AreEqual(3, deviation.Elements().Count());
        }

        /// <summary>
        /// Checks if the deviation statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void DeviationStatementWhitelistOverflowError()
        {
            var deviation = InterpreterCorrect.Root.Descendants("deviation").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => deviation.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviation.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviation.AddStatement(new ReferenceStatement()));
        }
    }
}