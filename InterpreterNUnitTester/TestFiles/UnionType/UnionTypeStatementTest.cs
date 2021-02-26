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
    public class UnionTypeStatement
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/UnionType/UnionTypeStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the union type is parsed correctly.
        /// </summary>
        [Test]
        public void UnionTypeIsParsedCorrectly()
        {
            var unionType = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "unionTest").Single().Elements().First();
            Assert.AreEqual(17, unionType.Elements().Count());
        }
    }
}