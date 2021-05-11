using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class IncludeStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/IncludeStatement/IncludeStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the include statement is parsed correctly.
        /// </summary>
        [Test]
        public void IncludeIsParsedCorrectly()
        {
            var include = InterpreterCorrect.Root.Descendants("include").Single();
            Assert.AreEqual(1, include.Elements().Count());
            Assert.AreEqual("mainTester", include.Argument);
        }

        /// <summary>
        /// Checks if the include statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void IncludeStatementWhitelistOverflowError()
        {
            var include = InterpreterCorrect.Root.Descendants("include").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => include.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => include.AddStatement(new RevisionDateStatement("1233.23.12")));
        }
    }
}