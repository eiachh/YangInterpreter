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
    public class ListStatementTestlate
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ListStatement/ListStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the list statement is parsed correctly.
        /// </summary>
        [Test]
        public void ListStatementIsParsedCorrectly()
        {
            var list = InterpreterCorrect.Root.Descendants("list").Where(statement => statement.Value == "mainTester").Single();
            Assert.AreEqual(21, list.Elements().Count());
        }

        /// <summary>
        /// Checks if the list statement is throwing arror at whitelist overflow.
        /// </summary>
        [Test]
        public void ListStatementWhitelistOverflowError()
        {
            var list = InterpreterCorrect.Root.Descendants("list").Where(statement => statement.Value == "mainTester").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new Int16TypeStatement("2")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new KeyStatement("ident")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new MaxElementsStatement("2")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new MinElementsStatement("2")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new OrderedByStatement("user")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new StatusStatement("current")));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddStatement(new WhenStatement("arg")));

        }
    }
}