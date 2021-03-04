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
    public class MandatoryStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/MandatoryStatement/MandatoryStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the mandatory statement is parsed correctly.
        /// </summary>
        [Test]
        public void MandatoryIsParsedCorrectly()
        {
            var mandatoryStatement = InterpreterCorrect.Root.Descendants("mandatory").First();
            Assert.AreEqual("true", mandatoryStatement.Value);
        }

        /// <summary>
        /// Checks if the default value is set to true when not given.
        /// </summary>
        [Test]
        public void MandatoryDefaultValue()
        {
            var mandatoryStatement = new MandatoryStatement();
            Assert.AreEqual("false", mandatoryStatement.Value);
        }

        /// <summary>
        /// Mandatory statement cannot have any child statement.
        /// </summary>
        [Test]
        public void MandatoryIsChildless()
        {
            var mandatoryStatement = InterpreterCorrect.Root.Descendants("mandatory").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => mandatoryStatement.AddStatement(new YangInterpreter.Statements.DescriptionStatement("text")));
        }

        /// <summary>
        /// Throws error at not proper value given.
        /// </summary>
        [Test]
        public void MandatoryImproperValue()
        {
            Assert.Throws<ImproperValue>(() => new MandatoryStatement("badValue"));
        }
    }
}