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
    public class LeafListStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/LeafListStatement/LeafListStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the leaf-list statement is parsed correctly.
        /// </summary>
        [Test]
        public void LeafListIsParsedCorrectly()
        {
            var leafList = InterpreterCorrect.Root.Descendants("leaf-list").Single();
            Assert.AreEqual(12, leafList.Elements().Count());
        }

        /// <summary>
        /// Checks if the leaf-list statement is throwing error at whitelist overflow
        /// </summary>
        [Test]
        public void LeafListWhitelistOverflowError()
        {
            var leafList = InterpreterCorrect.Root.Descendants("leaf-list").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new MaxElementsStatement("5")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new MinElementsStatement("3")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new OrderedByStatement("user")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new StatusStatement("current")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new UnitsStatement("unit desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new WhenStatement("arg of when")));
        }

        /// <summary>
        /// Checks if the leaf-list statement is throwing error when multiple types are added.
        /// </summary>
        [Test]
        public void LeafListTypeOverflowError()
        {
            var leafList = new LeafListStatement("tester");
            leafList.AddStatement(new Decimal64TypeStatement("23,6"));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new BooleanTypeStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new EmptyTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => leafList.AddStatement(new IdentityrefTypeStatement("arg")));
        }
    }
}