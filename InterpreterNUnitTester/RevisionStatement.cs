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
            var Revision = RevisipnStatementCorrect.Root.Descendants("revision").Single();
            Assert.AreEqual("2019-09-11", Revision.Value);
            Assert.AreEqual("Generic Session Control parameter file.", Revision.Descendants("description").Single().Value);
            Assert.AreEqual("The Reference for Revision \r\n2019-09-11", Revision.Descendants("reference").Single().Value);
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
            Revision TestRevision = new Revision("1997-09-02");
            TestRevision.AddStatement(new Reference());
            Assert.Throws<ArgumentOutOfRangeException>(() => TestRevision.AddStatement(new Reference()));
        }
    }
}