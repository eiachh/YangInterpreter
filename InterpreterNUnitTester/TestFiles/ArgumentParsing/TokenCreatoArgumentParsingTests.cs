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
    public class TokenCreatoArgumentParsingTests
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestCorrectCases.yang");
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void UnquotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "nonQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuoteConcatNormalQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "NormalQuoteConcatNormalQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuoteConcatSingleQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "NormalQuoteConcatSingleQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuoteConcatNormalQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "SingleQuoteConcatNormalQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuoteConcatSingleQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "SingleQuoteConcatSingleQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void RecursiveLongRandomConcatArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "RecursiveLongRandomConcatValid").Single().Elements().First();
            Assert.AreEqual("abcdefg", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNonQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "nonQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedNextLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nalso desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedNextLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nalso desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartSingleQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedSameLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartNormalQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedSameLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
            //var YangTestFile = YangInterpreterTool.Load("TestFiles/ModuleTests/RevisionStatementCorrect.yang");
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }
    }
}