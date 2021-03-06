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
    public class TypedefStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/TypedefStatement/TypedefStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision statement is parsed correctly.
        /// </summary>
        [Test]
        public void IsParsedCorrectly()
        {
            var typedef = InterpreterCorrect.Root.Elements("typedef").SingleOrDefault();
            Assert.AreEqual(6, typedef.Elements().Count());
        }

        /// <summary>
        /// Checks if the typedef statement is throwing error when multiple types are added.
        /// </summary>
        [Test]
        public void LeafListTypeOverflowError()
        {
            var typedef = new TypedefStatement("testerType");
            typedef.AddStatement(new Decimal64TypeStatement("23,6"));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new BooleanTypeStatement("true")));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new EmptyTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new IdentityrefTypeStatement("arg")));
        }

        /// <summary>
        /// Checks if the typedef statement is throwing error at whitelist overflow
        /// </summary>
        [Test]
        public void TypedefWhitelistOverflowError()
        {
            var typedef = InterpreterCorrect.Root.Elements("typedef").SingleOrDefault();
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new DefaultStatement("2")));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new ReferenceStatement("ref")));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new StatusStatement("current")));
            Assert.Throws<ArgumentOutOfRangeException>(() => typedef.AddStatement(new UnitsStatement("unit desc")));
        }
    }
}