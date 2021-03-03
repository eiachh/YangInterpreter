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
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuoteConcatNormalQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "NormalQuoteConcatNormalQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NormalQuoteConcatSingleQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "NormalQuoteConcatSingleQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuoteConcatNormalQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "SingleQuoteConcatNormalQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SingleQuoteConcatSingleQuoteArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "SingleQuoteConcatSingleQuoteValid").Single().Elements().First();
            Assert.AreEqual("ab", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void RecursiveLongRandomConcatArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "RecursiveLongRandomConcatValid").Single().Elements().First();
            Assert.AreEqual("abcdefg", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNonQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "nonQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedNextLineStartValid").Single().Elements().First();
            Assert.AreEqual("desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedNextLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedNextLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartSingleQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedSameLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartNormalQuotedMultilineArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedSameLineStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("desc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartNormalQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedSameLineStartMultilineConcattedValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SameLineStartSingleQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedSameLineConcattedStartMultilineValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartSingleQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "singleQuotedNextLineStartMultilineConcattedValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NextLineStartNormalQuotedMultilineConcattedArgParsedCorrectly()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "normalQuotedNextLineStartMultilineConcattedValid").Single().Elements().First();
            Assert.AreEqual("adesc\r\nmiddle\r\nmiddle2\r\nalso desc", desc.Value);
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void SpecialCharacterIgnoreCheck()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "SpecialCharacterCheck").Single().Elements().First();
            Assert.AreEqual("ade\\{s;'c\\\"\"", desc.Value);
        }

        /// <summary>
        /// Checks if concat is correct with completed quotes
        /// </summary>
        [Test]
        public void CompletedNormalQuoteConcattedMultiline()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "MultilineConcatWithCompletedNormalQuotes").Single().Elements().First();
            Assert.AreEqual("abc", desc.Value);
        }

        /// <summary>
        /// Checks if concat is correct with completed single quotes
        /// </summary>
        [Test]
        public void CompletedSingleQuoteConcattedMultiline()
        {
            var desc = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "MultilineConcatWithCompletedSingleQuotes").Single().Elements().First();
            Assert.AreEqual("abc", desc.Value);
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
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase6.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedSingleQuoteArgument()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase7.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedNormalQuoteConcattedArgument()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase8.yang"));
        }

        /// <summary>
        /// Checks if the unquoted arg value is parsed correctly.
        /// </summary>
        [Test]
        public void NonFinishedSingleQuoteConcattedArgument()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ArgumentParsing/ArgumentParseTestInvalidCase9.yang"));
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