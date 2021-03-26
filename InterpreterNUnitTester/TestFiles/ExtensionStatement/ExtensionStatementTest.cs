using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class ExtensionStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ExtensionStatement/ExtensionStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the extension statement is parsed correctly.
        /// </summary>
        [Test]
        public void ExtensionIsParsedCorrectly()
        {
            var ext = InterpreterCorrect.Root.Descendants("extension").Single();
            Assert.AreEqual(4, ext.Elements().Count());
        }

        /// <summary>
        /// Checks if the extension statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void ExtensionStatementWhitelistOverflowError()
        {
            var arg = InterpreterCorrect.Root.Descendants("argument").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new ArgumentStatement("arg")));
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => arg.AddStatement(new StatusStatement("current")));
        }
    }
}