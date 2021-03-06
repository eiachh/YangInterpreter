﻿using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;

namespace InterpreterNUnitTester
{
    public class DescriptionStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Description/DescriptionCorrect.yang");
        }

        /// <summary>
        /// Checks if the revision value is parsed correctly.
        /// </summary>
        [Test]
        public void DescriptionValueParsedCorrectly()
        {
            Assert.AreEqual("Description of correctly formatted\r\nmodule,\r\nwith multiline value.", InterpreterCorrect.Root.Elements("description").Single().Argument);
        }

        /// <summary>
        /// Checks if the revision value is formatted correctly at tostring.
        /// </summary>
        [Test]
        public void DescriptionValueFormattedCorrectlyAtOutput()
        {
            Assert.AreEqual("description\r\n\t\"Description of correctly formatted\r\n\tmodule,\r\n\twith multiline value.\";", InterpreterCorrect.Root.Elements("description").Single().ToString());
        }

        /// <summary>
        /// Throws error if we try to add Child statements.
        /// </summary>
        [Test]
        public void ThrowErrorOnStatementAdd()
        {
            YangInterpreter.Statements.DescriptionStatement desc = new YangInterpreter.Statements.DescriptionStatement("Some value");
            Assert.Throws<ArgumentOutOfRangeException>(() => desc.AddStatement(new YangInterpreter.Statements.DescriptionStatement()));
        }

        /// <summary>
        /// Throws error if description`s value`s first quotation mark is missing
        /// </summary>
        [Test]
        public void ThrowErrorOnFirstQuotationMarkMissing()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Description/DescriptionMissingFirstQuotationMark.yang"));
        }

        /// <summary>
        /// Throws error if description`s value`s second quotation mark is missing
        /// </summary>
        [Test]
        public void ThrowErrorOnSecondQuotationMarkMissing()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Description/DescriptionMissingSecondQuotationMark.yang"));
        }

        /// <summary>
        /// Throws error if description`s semicolon.
        /// </summary>
        [Test]
        public void ThrowErrorOnMissingSemicolon()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Description/DescriptionMissingSemicolon.yang"));
        }
    }
}