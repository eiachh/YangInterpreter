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
    public class IfFeatureStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/IfFeature/IfFeatureStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the IfFeature statement is parsed correctly.
        /// </summary>
        [Test]
        public void IfFeatureIsParsedCorrectly()
        {
            var ifFeature = InterpreterCorrect.Root.Descendants("if-feature").Single();
            Assert.AreEqual("myFeature", ifFeature.Value);
        }

        /// <summary>
        /// Throws an error if any statement is added to it.
        /// </summary>
        [Test]
        public void IfFeatureIsChildless()
        {
            var ifFeature = new IfFeatureStatement("feature");
            Assert.Throws<ArgumentOutOfRangeException>(() => ifFeature.AddStatement(new EmptyLineStatement()));
        }
    }
}