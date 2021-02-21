using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;

namespace InterpreterNUnitTester
{
    class ModuleStatements
    {
        YangInterpreterTool InterpreterCorrect;
        YangInterpreterTool InterpreterCorrectYangVerMissing;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsCorrect1.yang");
            InterpreterCorrectYangVerMissing = YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsCorrectYangVerMissing.yang");
        }

        /// <summary>
        /// Checks if the module name is parsed correctly from the yang file.
        /// </summary>
        [Test]
        public void ModuleNameParsedCorrectlyTest()
        {
            Assert.AreEqual("CorrectTestModule1", InterpreterCorrect.Root.Value);
        }

        /// <summary>
        /// Checks if the yangparserversion got parsed correctly from the file.
        /// </summary>
        [Test]
        public void YangVersionParsedCorrectly()
        {
            var Nodes = InterpreterCorrect.Root.Descendants("yang-version");
            Assert.IsTrue(Nodes.Count() > 0);
            var YangVersionNode = Nodes.First() as YangVersionStatement;
            Assert.AreEqual("1", YangVersionNode.Value);
        }

        /// <summary>
        /// Interpreter has to assume yang version is 1 if its not given.
        /// </summary>
        [Test]
        public void ModuleInterpreteMissingYangVersion()
        {
            var Nodes = InterpreterCorrectYangVerMissing.Root.Descendants("yang-version");
            Assert.IsTrue(Nodes.Count() > 0);
            var YangVersionNode = Nodes.First() as YangVersionStatement;
            Assert.AreEqual("1", YangVersionNode.Value);
        }

        /// <summary>
        /// Checks if the Namespace was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleNameSpaceParsedCorrectlyTest()
        {
            Assert.AreEqual("NamespaceCorrectTestModule1", InterpreterCorrect.Root.Descendants("namespace").Single().Value);
        }

        /// <summary>
        /// Checks if the prefix was parsed correctly.
        /// </summary>
        [Test]
        public void ModulePrefixParsedCorrectlyTest()
        {
            Assert.AreEqual("nctm", InterpreterCorrect.Root.Elements("prefix").Single().Value);
        }

        /// <summary>
        /// Checks if module imperts was successfuly parsed.
        /// </summary>
        [Test]
        public void ModuleImportWithPrefixParsedCorrectlyTest()
        {
            Assert.AreEqual("nctm", InterpreterCorrect.Root.GetPrefixByNamespace("CorrectTestModule1"));
            Assert.AreEqual("yani", InterpreterCorrect.Root.GetPrefixByNamespace("yang-interpreter"));
            Assert.AreEqual("in-fi", InterpreterCorrect.Root.GetPrefixByNamespace("interpreter-files"));
            Assert.AreEqual("profiles", InterpreterCorrect.Root.GetPrefixByNamespace("profiles"));

            Assert.AreEqual("CorrectTestModule1", InterpreterCorrect.Root.GetNamespace("nctm"));
            Assert.AreEqual("yang-interpreter", InterpreterCorrect.Root.GetNamespace("yani"));
            Assert.AreEqual("interpreter-files", InterpreterCorrect.Root.GetNamespace("in-fi"));
            Assert.AreEqual("profiles", InterpreterCorrect.Root.GetNamespace("profiles"));
        }

        /// <summary>
        /// Checks if the organization was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleOrganizationParsedCorrectlyTest()
        {
            Assert.AreEqual("My diplomwork corp", InterpreterCorrect.Root.Descendants("organization").Single().Value);
        }

        /// <summary>
        /// Checks if the contact was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleContactParsedCorrectlyTest()
        {
            Assert.AreEqual("Adam Sranko srankoadam@gmail.com", InterpreterCorrect.Root.Descendants("contact").Single().Value);
        }

        /// <summary>
        /// Checks if the description was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleDescriptionParsedCorrectlyTest()
        {
            Assert.AreEqual("Description of correctly formatted\r\nmodule,\r\nwith multiline value.", InterpreterCorrect.Root.Elements("description").Single().Value);
        }

        /// <summary>
        /// Checks if the reference was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleReferenceParsedCorrectlyTest()
        {
            
            Assert.AreEqual("Reference1102-2323", InterpreterCorrect.Root.Descendants("reference").Single().Value);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////IMPROPER FORMATION TESTS/////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Interpreter has to give InvalidYangVersion exception if  the version value is anything but 1.
        /// </summary>
        [Test]
        public void ModuleBadYangversionException()
        {
            Assert.Throws<InvalidYangVersion>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsInproperYangVer.yang"));
        }

        /// <summary>
        /// Interpreter has to supress InvalidYangVersion exception if  the version value is not 1 but InterpreterOption.Force is given.
        /// </summary>
        [Test]
        public void ModuleBadYangversionExceptionSupression()
        {
           YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsInproperYangVer.yang",InterpreterOption.Force);
        }

        /// <summary>
        /// Interpreter has to give error in worngly formatted import line.
        /// </summary>
        [Test]
        public void ModuleInterpreterImportInnerMalformFailException()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Import/InterpreterMalformedImportInner.yang"));
        }

        /// <summary>
        /// Interpreter has to give error in worngly formatted import line.
        /// </summary>
        [Test]
        public void ModuleInterpreterImportEndMissingFailException()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Import/InterpreterMalformedImportInnerMissingEnd.yang"));
        }

        /// <summary>
        /// Interpreter has to give error in worngly formatted import line.
        /// </summary>
        [Test]
        public void ModuleInterpreterImportOutterMalformFailException()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/Import/InterpreterMalformedImportOutter.yang"));
        }

        /// <summary>
        /// Interpreter has to give error if a value is not finished with a ; symbol.
        /// </summary>
        [Test]
        public void ModuleInterpreterMissingRowEndSymbol()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/InterpreterMissingSemicolonInValue.yang"));
        }

        /// <summary>
        /// Interpreter has to give error if yang version is malformed.
        /// </summary>
        [Test]
        public void ModuleInterpreteMalformedYangVersion()
        {
            Assert.Throws<InterpreterParseFail>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsMalformedYangVer.yang"));
        }
    }
}