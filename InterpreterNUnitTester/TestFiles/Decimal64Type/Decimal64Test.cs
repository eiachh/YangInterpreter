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
    public class Decimal64Test
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Decimal64Type/Decimal64TypeCorrect.yang");
        }

        /// <summary>
        /// Checks if the decimal64 type is parsed correctly.
        /// </summary>
        [Test]
        public void Decimal64WithSubstatementIsParsedCorrectly()
        {
            var decimal64Type = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "decimalTest1").Single().Elements().First();
            Assert.AreEqual("decimal64", decimal64Type.Argument);
            Assert.AreEqual(1, decimal64Type.Elements().Count());
        }

        /// <summary>
        /// Checks if the decimal64 type is parsed correctly.
        /// </summary>
        [Test]
        public void Decimal64WithoutSubstatementIsParsedCorrectly()
        {
            var decimal64Type = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "decimalTest2").Single().Elements().First();
            Assert.AreEqual("decimal64", decimal64Type.Argument);
            Assert.AreEqual(0, decimal64Type.Elements().Count());
            Assert.AreEqual("type decimal64;", decimal64Type.ToString());
        }
    }
}