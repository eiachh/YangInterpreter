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
    public class UsesStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/UsesStatement/UsesStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the uses statement is parsed correctly.
        /// </summary>
        [Test]
        public void UsesStatementIsParsedCorrectly()
        {
            var uses = InterpreterCorrect.Root.Descendants("uses").Single();
            Assert.AreEqual(7, uses.Elements().Count());
        }

        /// <summary>
        /// Checks if the uses statement is throwing error at overflown whitelisted elements.
        /// </summary>
        [Test]
        public void UsesStatementWhitelistOverflowError()
        {
            var uses = InterpreterCorrect.Root.Descendants("uses").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => uses.AddStatement(new AugmentStatement("name")));
            Assert.Throws<ArgumentOutOfRangeException>(() => uses.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => uses.AddStatement(new RefineStatement("ident")));
            Assert.Throws<ArgumentOutOfRangeException>(() => uses.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => uses.AddStatement(new StatusStatement("current")));
            Assert.Throws<ArgumentOutOfRangeException>(() => uses.AddStatement(new WhenStatement("arg")));
        }
    }
}