using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class TypeStatement
    {
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Checks if the type with empty argument is parsed correctly.
        /// </summary>
        [Test]
        public void EmptyTypeStatementParsedCorrectly()
        {
            YangInterpreterTool interpreted = YangInterpreterTool.Load("TestFiles/TypeStatement/TypeStatementCorrect.yang");
            var leaf = interpreted.Root.Descendants("leaf").Where(statement => statement.Value == "emptyTest").FirstOrDefault();
            var typeEmpty = leaf.Elements().FirstOrDefault();
            Assert.AreEqual("empty", typeEmpty.Value);
            Assert.AreEqual(0, typeEmpty.Elements().Count());
            Assert.AreEqual("type empty;", typeEmpty.ToString());
        }

        /// <summary>
        /// Checks if the type with empty argument is parsed correctly.cannot get substatements.
        /// </summary>
        [Test]
        public void EmptyTypeStatementNoSubstatementAllowed()
        {
            EmptyTypeStatement ts = new EmptyTypeStatement();
            Assert.Throws<ArgumentOutOfRangeException>(() => ts.AddStatement(new EnumTypeStatement()));
        }

        /// <summary>
        /// Throws error if type empty is used as a container.
        /// </summary>
        [Test]
        public void EmptyTypeStatementMalformed()
        { 
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/TypeStatement/TypeStatementMalformed.yang"));
        }

        /// <summary>
        /// Checks if the bit type statement is parsed correctly.
        /// </summary>
        [Test]
        public void TypeStatementBitParsedCorrectly()
        {
            YangInterpreterTool InterpreterCorrect = YangInterpreterTool.Load("TestFiles/TypeStatement/TypeStatementBitTypeCorrect.yang");
        }

        /// <summary>
        /// Checks if the string type statement is parsed correctly.
        /// </summary>
        [Test]
        public void TypeStatementStringParsedCorrectly()
        {
            YangInterpreterTool InterpreterCorrect = YangInterpreterTool.Load("TestFiles/TypeStatement/TypeStatementStringTypeCorrect.yang");
            var typeString = InterpreterCorrect.Root.Descendants("type").First();
            Assert.AreEqual("string", typeString.Value);
            Assert.AreEqual("Length", typeString.Elements().First().Name);
            Assert.AreEqual(1, typeString.Elements().Count());
        }

        /// <summary>
        /// Checks if the enum statement is parsed correctly.
        /// </summary>
        [Test]
        public void EnumerationTypeIsParsedCorrectly()
        {
            YangInterpreterTool EnumStatementParser= YangInterpreterTool.Load("TestFiles/EnumStatement/EnumStatementCorrect.yang");
            var enumerationType = EnumStatementParser.Root.Descendants("type").Single();
            var elementsOfEnumeration = enumerationType.Elements();
            Assert.AreEqual(3, elementsOfEnumeration.Count());
            Assert.AreEqual("zero", elementsOfEnumeration.ToList()[0].Value);
            Assert.AreEqual("one", elementsOfEnumeration.ToList()[1].Value);
            Assert.AreEqual("seven", elementsOfEnumeration.ToList()[2].Value);
        }
    }
}