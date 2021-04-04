using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements.BaseStatements;

namespace InterpreterNUnitTester
{
    public class RevisionDateStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/RevisionDateStatement/RevisionDateStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision-date statement is parsed correctly.
        /// </summary>
        [Test]
        public void RevisionDateIsParsedCorrectly()
        {
            var revDate = InterpreterCorrect.Root.Descendants("revision-date").Single();
            Assert.AreEqual("2008-08-08", revDate.Value);
        }

        /// <summary>
        /// Checks if the revision statement is childless.
        /// </summary>
        [Test]
        public void RevisionDateIsChildless()
        {
            var revDate = InterpreterCorrect.Root.Descendants("revision-date").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => revDate.AddStatement(new EmptyLineStatement()));
        }
    }
}