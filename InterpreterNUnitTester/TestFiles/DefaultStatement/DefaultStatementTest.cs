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
    public class DefaultStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/DefaultStatement/DefaultStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the leaf`s default statement is parsed correctly.
        /// </summary>
        [Test]
        public void LeafsDefaultIsParsedCorrectly()
        {
            var leaf = InterpreterCorrect.Root.Elements("leaf").Single();
            Assert.AreEqual(2, leaf.Elements().Count());
            var def1 = leaf.Elements("default").First();
            Assert.AreEqual("2", def1.Value);
        }

        /// <summary>
        /// Checks if the choice`s default statement is parsed correctly.
        /// </summary>
        [Test]
        public void ChoicesDefaultIsParsedCorrectly()
        {
            var choice = InterpreterCorrect.Root.Descendants("choice").Single();
            Assert.AreEqual(2, choice.Elements().Count());
            var def2 = choice.Elements("default").First();
            Assert.AreEqual("caseName", def2.Value);
        }

        /// <summary>
        /// Checks if the default statement cannot have child statements.
        /// </summary>
        [Test]
        public void DefaultIsChildless()
        {
            var def = new DefaultStatement("arg");
            Assert.Throws<ArgumentOutOfRangeException>(() => def.AddStatement(new EmptyLineStatement()));
        }
    }
}