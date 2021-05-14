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
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "nonQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "singleQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "normalQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuoteConcatNormalQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "NormalQuoteConcatNormalQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuoteConcatSingleQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "NormalQuoteConcatSingleQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuoteConcatNormalQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "SingleQuoteConcatNormalQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuoteConcatSingleQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "SingleQuoteConcatSingleQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void RecursiveLongRandomConcatArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "RecursiveLongRandomConcatValid").Single().Elements().First();
            Assert.AreEqual("abcdefg", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNonQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "nonQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "normalQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "singleQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "normalQuotedNextLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "singleQuotedNextLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartSingleQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "singleQuotedSameLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartNormalQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "normalQuotedSameLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartNormalQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "normalQuotedSameLineStartMultilineConcattedValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartSingleQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "singleQuotedSameLineConcattedStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "singleQuotedNextLineStartMultilineConcattedValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "normalQuotedNextLineStartMultilineConcattedValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SpecialCharacterIgnoreCheck()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "SpecialCharacterCheck").Single().Elements().First();
            Assert.AreEqual("ade\\{s;'c\\\"\"", desc.Argument);
        }

        /// <summary>
        /// Checks if concat is correct with completed quotes
        /// </summary>
        [Test]
        public void CompletedNormalQuoteConcattedMultiline()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "MultilineConcatWithCompletedNormalQuotes").Single().Elements().First();
            Assert.AreEqual("abc", desc.Argument);
        }

        /// <summary>
        /// Checks if concat is correct with completed single quotes
        /// </summary>
        [Test]
        public void CompletedSingleQuoteConcattedMultiline()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "MultilineConcatWithCompletedSingleQuotes").Single().Elements().First();
            Assert.AreEqual("abc", desc.Argument);
        }
        /// <summary>
        /// Checks if argument contains a statement definition, it is still parsed as value.
        /// </summary>
        [Test]
        public void StatementAsText()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "statementAsText").Single().Elements().First();
            Assert.AreEqual("module fakeStatement {", desc.Argument);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void InvalidQuoteInSingleQuotedArgument()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase1.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void InvalidQuoteInNormalQuotedArgument()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase2.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void InvalidQuoteInNonQuotedArgument1()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase3.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void InvalidQuoteInNonQuotedArgument2()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase4.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void InvalidConcatWithNonQuotedArgument()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase5.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedNormalQuoteArgument()
        {
            Assert.Throws<StatementEndIsMissing>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase6.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedSingleQuoteArgument()
        {
            Assert.Throws<StatementEndIsMissing>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase7.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedNormalQuoteConcattedArgument()
        {
            Assert.Throws<StatementEndIsMissing>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase8.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedSingleQuoteConcattedArgument()
        {
            Assert.Throws<StatementEndIsMissing>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase9.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void DifferentQuoteFinishThanBeg()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase10.yang"));
        }
    }
}