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
    public class MustStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/MustStatement/MustStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the must statement is parsed correctly.
        /// </summary>
        [Test]
        public void MustIsParsedCorrectly()
        {
            var mustStatement = InterpreterCorrect.Root.Descendants("must").SingleOrDefault();
            Assert.AreEqual("ifType != 'ethernet' or (ifType = 'ethernet' and ifMTU = 1500)", mustStatement.Argument);

        }

        /// <summary>
        /// Must statement whitelisted elements add check.
        /// </summary>
        [Test]
        public void MustWhitelistedElementsCheck()
        {
            var mustStatement = InterpreterCorrect.Root.Descendants("must").SingleOrDefault();
            Assert.AreEqual(4, mustStatement.Elements().Count());
        }

        /// <summary>
        /// Must statement whitelisted elements throw error after the specified limit.
        /// </summary>
        [Test]
        public void MustWhitelistOverloadCheck()
        {
            var mustStatement = InterpreterCorrect.Root.Descendants("must").SingleOrDefault();
            Assert.Throws<ArgumentOutOfRangeException>(() => mustStatement.AddStatement(new YangInterpreter.Statements.DescriptionStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => mustStatement.AddStatement(new ErrorAppTagStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => mustStatement.AddStatement(new ErrorMessageStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => mustStatement.AddStatement(new ReferenceStatement()));
        }

        /// <summary>
        /// Must statement cannot add other elements than whitelisted ones.
        /// </summary>
        [Test]
        public void MustNonWhitelistedElemAddError()
        {
            var mustStatement = InterpreterCorrect.Root.Descendants("must").SingleOrDefault();
            Assert.Throws<ArgumentOutOfRangeException>(() => mustStatement.AddStatement(new LeafStatement()));
        }
    }
}