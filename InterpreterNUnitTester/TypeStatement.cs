using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class TypeStatement
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            //InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionIsParsedCorrectly()
        {
            EmptyTypeStatement ts = new EmptyTypeStatement();
            ts.AddStatement(new EnumTypeStatement());
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
        }
    }
}