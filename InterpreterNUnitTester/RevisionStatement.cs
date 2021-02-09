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
    class RevisionStatement
    {
        YangInterpreterTool RevisipnStatementCorrect;
        [SetUp]
        public void Setup()
        {
            RevisipnStatementCorrect = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionIsParsedCorrectly()
        {
           RevisipnStatementCorrect = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
           Assert.AreEqual("2019-09-11", RevisipnStatementCorrect.Root.DescendantsNode("revision").Single().Value);
        }

        /// <summary>
        /// If the Revision Value is not correct has to throw InvalidArgument exception.
        /// </summary>
        [Test]
        public void RevisionImproperValue()
        {
            Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Revision cannot have more than 1 Description Statement
        /// </summary>
        [Test]
        public void RevisionMultipleDescriptionStatementError()
        {
            Revision TestRevision = new Revision("1997-09-02");
            TestRevision.AddStatement(new Description());
            Assert.Throws<ArgumentOutOfRangeException>(() => TestRevision.AddStatement(new Description()));
        }

        /// <summary>
        /// Revision cannot have more than 1 Reference Statement
        /// </summary>
        [Test]
        public void RevisionMultipleReferenceStatementError()
        {
            //FINISh AFTER REFERENCE IS CREATED
            Assert.IsTrue(false);
            Revision TestRevision = new Revision("1997-09-02");
            TestRevision.AddStatement(new Description());
            Assert.Throws<ArgumentOutOfRangeException>(() => TestRevision.AddStatement(new Description()));
        }
    }
}