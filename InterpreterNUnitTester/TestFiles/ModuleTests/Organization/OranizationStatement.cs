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
    public class OrganizationStatement
    {
        YangInterpreterTool InterpreterOrganizationSingleLine;
        YangInterpreterTool InterpreterOrganizationSameLineStart;
        YangInterpreterTool InterpreterOrganizationNextLineStart;

        [SetUp]
        public void Setup()
        {
            InterpreterOrganizationSingleLine = YangInterpreterTool.Load("TestFiles/ModuleTests/Organization/OrganizationCorrect.yang");
            InterpreterOrganizationSameLineStart = YangInterpreterTool.Load("TestFiles/ModuleTests/Organization/OrganizationCorrectSameLineStart.yang");
            InterpreterOrganizationNextLineStart = YangInterpreterTool.Load("TestFiles/ModuleTests/Organization/OrganizationCorrectNextLineStart.yang");
        }

        /// <summary>
        /// Checks if the organization value is correct and if formatted correctly
        /// </summary>
        [Test]
        public void OrganizationCorrectValueSingleLine()
        {
            var Org = InterpreterOrganizationSingleLine.Root.Descendants("organization").Single();
            Assert.AreEqual("My diplomwork corp", Org.Value);
            var OrgAsString = Org.ToString();
            Assert.AreEqual("organization \"My diplomwork corp\";",OrgAsString);
        }

        /// <summary>
        /// Checks if the organization value is correct and if formatted correctly
        /// </summary>
        [Test]
        public void OrganizationCorrectValueSameLineStart()
        {
            var Org = InterpreterOrganizationSameLineStart.Root.Descendants("organization").Single();
            Assert.AreEqual("My diplomwork \r\nother line corp", Org.Value);
            var OrgAsString = Org.ToString();
            Assert.AreEqual("organization \"My diplomwork \r\n\tother line corp\";",OrgAsString);
        }

        /// <summary>
        /// Checks if the organization value is correct and if formatted correctly
        /// </summary>
        [Test]
        public void OrganizationCorrectValueNextLineStart()
        {
            var Org = InterpreterOrganizationNextLineStart.Root.Descendants("organization").Single();
            Assert.AreEqual("My diplomwork \r\nother line corp", Org.Value);
            Assert.AreEqual("organization \r\n\t\"My diplomwork \r\n\tother line corp\";",Org.StatementAsYangString());
        }

        /// <summary>
        /// Throws error if organisation value is not properly formatted.
        /// </summary>
        [Test]
        public void OrganizationMissingFirstQuotationMark()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/Organization/OrganizationMissingFirstQuotationMark.yang"));
        }

        /// <summary>
        /// Throws error if organisation value is not properly formatted.
        /// </summary>
        [Test]
        public void OrganizationMissingSecondQuotationMark()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/Organization/OrganizationMissingSecondQuotationMark.yang"));
        }

        /// <summary>
        /// Throws error if organisation semicolon is missing from the end of value.
        /// </summary>
        [Test]
        public void OrganizationMissingSemicolon()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/Organization/OrganizationMissingSemicolon.yang"));
        }

        /// <summary>
        /// Throws error if we try to add any child.
        /// </summary>
        [Test]
        public void OrganizationAddStatementFailCheck()
        {
            Organization org = new Organization("SomeValue");
            Assert.Throws<ArgumentOutOfRangeException>(() => org.AddStatement(new EmptyLineStatement()));
        }
    }
}