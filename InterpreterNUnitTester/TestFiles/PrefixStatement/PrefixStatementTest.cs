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
    public class PrefixStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/PrefixStatement/PrefixStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the prefix statement is parsed correctly.
        /// </summary>
        [Test]
        public void PrefixIsParsedCorrectly()
        {
            var pref = InterpreterCorrect.Root.Descendants("prefix").Single(statement => statement.Argument == "someVal");
            Assert.AreEqual("someVal", pref.Argument);
        }

        /// <summary>
        /// Checks if the prefix statement is childless.
        /// </summary>
        [Test]
        public void PrefixIsChildless()
        {
            var pref = InterpreterCorrect.Root.Descendants("prefix").Single(statement => statement.Argument == "someVal");
            Assert.Throws<ArgumentOutOfRangeException>(() => pref.AddStatement(new EmptyLineStatement()));
        }

        /// <summary>
        /// Checks if the prefix statement is registering in the module.
        /// </summary>
        [Test]
        public void PrefixRootRegistrationTest()
        {
            var pref = InterpreterCorrect.Root.Descendants("prefix").Single(statement => statement.Argument == "someVal");
            Assert.AreEqual("tester",InterpreterCorrect.Root.NamespaceDictionary["someVal"]);
        }

        /// <summary>
        /// Checks if the prefix statement is refreshing in the module after change.
        /// </summary>
        [Test]
        public void PrefixRootRegistrationRefreshOnChange()
        {
            var pref = InterpreterCorrect.Root.Descendants("prefix").Single(statement => statement.Argument == "someVal");
            Assert.AreEqual("tester", InterpreterCorrect.Root.NamespaceDictionary["someVal"]);
            pref.Argument = "newVal";
            Assert.AreEqual("tester", InterpreterCorrect.Root.NamespaceDictionary["newVal"]);
        }
    }
}