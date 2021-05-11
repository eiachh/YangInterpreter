using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class IdentityStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/IdentityStatement/IdentityStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the identity statement is parsed correctly.
        /// </summary>
        [Test]
        public void IdentityIsParsedCorrectly()
        {
            var ident = InterpreterCorrect.Root.Descendants("identity").Single();
            Assert.AreEqual(4, ident.Elements().Count());
            Assert.AreEqual("mainTester", ident.Argument);
        }

        /// <summary>
        /// Checks if the identity statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void IdentityStatementWhitelistOverflowError()
        {
            var ident = InterpreterCorrect.Root.Descendants("identity").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => ident.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => ident.AddStatement(new BaseStatement("identifier")));
            Assert.Throws<ArgumentOutOfRangeException>(() => ident.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => ident.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => ident.AddStatement(new StatusStatement("deprecated")));
        }
    }
}