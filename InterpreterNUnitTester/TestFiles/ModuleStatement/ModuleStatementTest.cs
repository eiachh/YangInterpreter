using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class ModuleStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ModuleStatement/ModuleStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the module statement is parsed correctly.
        /// </summary>
        [Test]
        public void ModuleIsParsedCorrectly()
        {
            var module = InterpreterCorrect.Root;
            Assert.AreEqual("ModuleStatementCorrect", module.Value);
            Assert.AreEqual(26, module.Elements().Count());
        }

        /// <summary>
        /// Checks if the augment statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void AugmentStatementWhitelistOverflowError()
        {
            var module = InterpreterCorrect.Root;
            Assert.Throws<ArgumentOutOfRangeException>(() => module.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => module.AddStatement(new ContactStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => module.AddStatement(new DescriptionStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => module.AddStatement(new OrganizationStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => module.AddStatement(new PrefixStatement("pref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => module.AddStatement(new ReferenceStatement()));
        }
    }
}