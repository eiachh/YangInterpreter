using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class DeviateStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/DeviateStatement/DeviateStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the deviate statement is parsed correctly.
        /// </summary>
        [Test]
        public void DeviateIsParsedCorrectly()
        {
            var deviate = InterpreterCorrect.Root.Descendants("deviate").Where(statement => statement.Value == "mainTester").Single();
            Assert.AreEqual(9, deviate.Elements().Count());
        }

        /// <summary>
        /// Checks if the deviate statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void DeviateStatementWhitelistOverflowError()
        {
            var deviate = InterpreterCorrect.Root.Descendants("deviate").Where(statement => statement.Value == "mainTester").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new DefaultStatement("mainTester")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new MandatoryStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new MaxElementsStatement("6")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new MinElementsStatement("3")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new UnitsStatement("desc")));
        }

        /// <summary>
        /// Checks if the deviate statement is throwing error at overflowing type elements.
        /// </summary>
        [Test]
        public void DeviateStatementSingleTypeHoldingTest()
        {
            var deviate = new LeafStatement("name");
            deviate.AddStatement(new BitsTypeStatement("bitType"));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new Int32TypeStatement("32")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new BooleanTypeStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => deviate.AddStatement(new EmptyTypeStatement()));
        }
    }
}