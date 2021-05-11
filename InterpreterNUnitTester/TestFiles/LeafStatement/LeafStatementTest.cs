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
    public class LeafStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/LeafStatement/LeafStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the leaf statement is parsed correctly.
        /// </summary>
        [Test]
        public void LeafStatementIsParsedCorrectly()
        {
            var leaf = InterpreterCorrect.Root.Descendants("leaf").Single();
            Assert.AreEqual(11, leaf.Elements().Count());
            Assert.AreEqual("tester", leaf.Argument);
        }

        /// <summary>
        /// Checks if the leaf statement is throwing error at overflowing elements.
        /// </summary>
        [Test]
        public void LeafStatementWhitelistOverflowTest()
        {
            var leaf = InterpreterCorrect.Root.Descendants("leaf").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new DefaultStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new DescriptionStatement("Some desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new MandatoryStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new StatusStatement("current")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new UnitsStatement("another unit desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new WhenStatement("xpath")));
        }

        /// <summary>
        /// Checks if the leaf statement is throwing error at overflowing type elements.
        /// </summary>
        [Test]
        public void LeafStatementSingleTypeHoldingTest()
        {
            var leaf = new LeafStatement("name");
            leaf.AddStatement(new BitsTypeStatement("bitType"));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new Int32TypeStatement("32")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new BooleanTypeStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leaf.AddStatement(new EmptyTypeStatement()));
        }
    }
}