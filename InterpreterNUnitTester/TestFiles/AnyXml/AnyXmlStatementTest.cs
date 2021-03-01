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
    public class AnyXmlStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/AnyXml/AnyXmlStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the anyxml is parsed correctly.
        /// </summary>
        [Test]
        public void AnyXmlStatementIsParsedCorrectly()
        {
            var dsdsds = 2;
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }
    }
}