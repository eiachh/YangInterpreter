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
    public class FeatureStatemenTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/FeatureStatement/FeatureStatemenCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision statement is parsed correctly.
        /// </summary>
        [Test]
        public void IsParsedCorrectly()
        {
            var feature = InterpreterCorrect.Root.Descendants("feature").Single(statement => statement.Value == "mainTester");
            Assert.AreEqual(4, feature.Elements().Count());
        }
    }
}