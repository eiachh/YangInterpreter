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
    class RevisionStatementTest
    {
        YangInterpreterTool RevisipnStatementCorrect;
        YangInterpreterTool RevisipnStatementCorrectChildless;
        [SetUp]
        public void Setup()
        {
            RevisipnStatementCorrect = YangInterpreterTool.Load("TestFiles/RevisionStatement/RevisionStatementCorrect.yang");
           
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionIsParsedCorrectly()
        {
            RevisipnStatementCorrect = YangInterpreterTool.Load("TestFiles/RevisionStatement/RevisionStatementCorrect.yang");
            var Revision = RevisipnStatementCorrect.Root.Descendants("revision").Single();
            Assert.AreEqual("2019-09-11", Revision.Argument);
            Assert.AreEqual("Generic Session Control parameter file.", Revision.Descendants("description").Single().Argument);
            Assert.AreEqual("The Reference for Revision \r\n2019-09-11", Revision.Descendants("reference").Single().Argument);
        }

        /// <summary>
        /// If the Revision Value is not correct has to throw InvalidArgument exception.
        /// </summary>
        [Test]
        public void RevisionImproperValue()
        {
            Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/RevisionStatement/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Revision cannot have more than 1 Description Statement
        /// </summary>
        [Test]
        public void RevisionMultipleDescriptionStatementError()
        {
            RevisionStatement TestRevision = new RevisionStatement("1997-09-02");
            TestRevision.AddStatement(new YangInterpreter.Statements.DescriptionStatement());
            Assert.Throws<ArgumentOutOfRangeException>(() => TestRevision.AddStatement(new YangInterpreter.Statements.DescriptionStatement()));
        }

        /// <summary>
        /// Revision cannot have more than 1 Reference Statement
        /// </summary>
        [Test]
        public void RevisionMultipleReferenceStatementError()
        {
            RevisionStatement TestRevision = new RevisionStatement("1997-09-02");
            TestRevision.AddStatement(new ReferenceStatement());
            Assert.Throws<ArgumentOutOfRangeException>(() => TestRevision.AddStatement(new ReferenceStatement()));
        }

        /// <summary>
        /// Revision without childs interpreted correctly.
        /// </summary>
        [Test]
        public void RevisionChildless()
        {
            RevisipnStatementCorrectChildless = YangInterpreterTool.Load("TestFiles/RevisionStatement/RevisionStatementCorrectChildless.yang");
            var rev = RevisipnStatementCorrectChildless.Root.Descendants("revision").Single();
            Assert.AreEqual("2019-09-11", rev.Argument);
            Assert.AreEqual("revision 2019-09-11;", rev.ToString());
        }
    }
}