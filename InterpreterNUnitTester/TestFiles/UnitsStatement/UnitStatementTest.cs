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
    public class UnitStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/UnitsStatement/UnitsStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the units statement is parsed correctly.
        /// </summary>
        [Test]
        public void UnitsStatementIsParsedCorrectly()
        {
            var unit = InterpreterCorrect.Root.Descendants("units").Single();
            Assert.AreEqual("desc of unit", unit.Value);
        }
    }
}