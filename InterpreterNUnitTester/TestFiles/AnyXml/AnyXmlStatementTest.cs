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
    public class AnyXmlStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/AnyXml/AnyXmlStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the anyxml is parsed correctly.
        /// </summary>
        [Test]
        public void AnyXmlStatementIsParsedCorrectly()
        {
            var anyxml = InterpreterCorrect.Root.Descendants("anyxml").First();
            Assert.AreEqual(8,anyxml.Elements().Count());
            Assert.AreEqual("identifier", anyxml.Argument);
        }

        /// <summary>
        /// Checks if the anyxml is throwing exception if whitelist max number is exceeded.
        /// </summary>
        [Test]
        public void AnyXmlStatementWhitelistOverloadCheck()
        {
            var anyxml = InterpreterCorrect.Root.Descendants("anyxml").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new ConfigStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new YangInterpreter.Statements.DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new MandatoryStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new StatusStatement("current")));
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new WhenStatement("whenStatement")));
        }

        /// <summary>
        /// Checks if the anyxml is throwing exception if non whitelisted element is added.
        /// </summary>
        [Test]
        public void AnyXmlStatementNonWhitelistedStatementAdd()
        {
            var anyxml = InterpreterCorrect.Root.Descendants("anyxml").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => anyxml.AddStatement(new LeafStatement("leaf1")));
        }
    }
}