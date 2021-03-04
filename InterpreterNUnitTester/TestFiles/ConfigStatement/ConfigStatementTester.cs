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
    public class ConfigStatementTester
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ConfigStatement/ConfigStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the config statement is parsed correctly.
        /// </summary>
        [Test]
        public void ConfigIsParsedCorrectly()
        {
            var configStatement = InterpreterCorrect.Root.Descendants("config").First();
            Assert.AreEqual("false", configStatement.Value);
        }

        /// <summary>
        /// Checks if the default value is set to true when not given.
        /// </summary>
        [Test]
        public void ConfigDefaultValue()
        {
            var configStatement = new ConfigStatement();
            Assert.AreEqual("true", configStatement.Value);
        }

        /// <summary>
        /// Config statement cannot have any child statement.
        /// </summary>
        [Test]
        public void ConfigIsChildless()
        {
            var configStatement = InterpreterCorrect.Root.Descendants("config").First();
            Assert.Throws<ArgumentOutOfRangeException>(() => configStatement.AddStatement(new YangInterpreter.Statements.DescriptionStatement("text")));
        }

        /// <summary>
        /// Throws error at not proper value given.
        /// </summary>
        [Test]
        public void ConfigImproperValue()
        {
            Assert.Throws<ImproperValue>(() => new ConfigStatement("badValue"));
        }
    }
}