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
    public class PathStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void PathIsParsedCorrectly()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/PathStatement/LeafRefTypeStatementCorrect.yang");
            var typeStatement = InterpreterCorrect.Root.Descendants("type").Single();
            var pathStatement = typeStatement.Elements().Single();
            Assert.AreEqual("/interface/name", pathStatement.Argument);
            Assert.AreEqual("path \"/interface/name\";", pathStatement.ToString());
        }
    }
}