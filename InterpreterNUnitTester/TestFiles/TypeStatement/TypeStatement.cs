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
        //YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            //InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionIsParsedCorrectly()
        {
            EmptyTypeStatement ts = new EmptyTypeStatement();
            ts.AddStatement(new EnumTypeStatement());
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void TypeStatementBitCorrect()
        {
            YangInterpreterTool InterpreterCorrect = YangInterpreterTool.Load("TestFiles/TypeStatement/TypeStatementBitTypeCorrect.yang");
            
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
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