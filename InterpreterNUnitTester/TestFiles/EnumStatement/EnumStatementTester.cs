using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;

namespace InterpreterNUnitTester
{
    public class EnumStatementTester
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/EnumStatement/EnumStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the enum statement is parsed correctly.
        /// </summary>
        [Test]
        public void EnumStatementIsParsedCorrectly()
        {
            var enumerationType = InterpreterCorrect.Root.Descendants("type").Single();
            var elementsOfEnumeration = enumerationType.Elements();
            var enumSubstatements = elementsOfEnumeration.Last().Descendants("").ToList();

            Assert.AreEqual("current", enumSubstatements[0].Value);
            Assert.AreEqual("7", enumSubstatements[1].Value);
            Assert.AreEqual("Reference of enum seven.", enumSubstatements[2].Value);
            Assert.AreEqual("Description of enum seven.", enumSubstatements[3].Value);
        }

        /// <summary>
        /// Checks if the enum statement can only have one status statement.
        /// </summary>
        [Test]
        public void EnumStatementOverloadOfStatus()
        {
            var enumStatement = InterpreterCorrect.Root.Descendants("type").Single().Descendants("enum").Where(statement => statement.Value == "seven").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => enumStatement.AddStatement(new StatusStatement("current")));
        }

        /// <summary>
        /// Checks if the enum statement can only have one value statement.
        /// </summary>
        [Test]
        public void EnumStatementOverloadOfValue()
        {
            var enumStatement = InterpreterCorrect.Root.Descendants("type").Single().Descendants("enum").Where(statement => statement.Value == "seven").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => enumStatement.AddStatement(new ValueStatement("2")));
        }

        /// <summary>
        /// Checks if the enum statement can only have one reference statement.
        /// </summary>
        [Test]
        public void EnumStatementOverloadOfReference()
        {
            var enumStatement = InterpreterCorrect.Root.Descendants("type").Single().Descendants("enum").Where(statement => statement.Value == "seven").First(); 
            Assert.Throws<ArgumentOutOfRangeException>(() => enumStatement.AddStatement(new ReferenceStatement("some text in reference")));
        }

        /// <summary>
        /// Checks if the enum statement can only have one description statement.
        /// </summary>
        [Test]
        public void EnumStatementOverloadOfDescription()
        {
            var enumStatement = InterpreterCorrect.Root.Descendants("type").Single().Descendants("enum").Where(statement => statement.Value == "seven").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => enumStatement.AddStatement(new YangInterpreter.Statements.DescriptionStatement("some text in description")));
        }

        /// <summary>
        /// Checks if the enum statement formatted correctly at toString()
        /// </summary>
        [Test]
        public void EnumToStringFormatChecking()
        {
            var enumStatementChildless = InterpreterCorrect.Root.Descendants("type").Single().Descendants("enum").Where(statement => statement.Value == "one").First();
            var enumStatementWithChildren = InterpreterCorrect.Root.Descendants("type").Single().Descendants("enum").Where(statement => statement.Value == "seven").First();
            Assert.AreEqual("enum one;", enumStatementChildless.ToString());
            Assert.AreEqual("enum seven {\r\n\tstatus current;\r\n\tvalue 7;\r\n\treference \"Reference of enum seven.\";\r\n\tdescription \"Description of enum seven.\";\r\n}", enumStatementWithChildren.ToString());
        }
    }
}