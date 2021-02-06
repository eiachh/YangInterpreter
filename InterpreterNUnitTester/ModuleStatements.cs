using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Nodes;
using YangInterpreter.Nodes.BaseNodes;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;

namespace InterpreterNUnitTester
{
    public class ModuleStatements
    {
        YangInterpreterTool InterpreterCorrect;
        YangInterpreterTool InterpreterInproperYangVersion;
        YangInterpreterTool InterpreterMissingSemicolonInValue;
        YangInterpreterTool InterpreterInproperMalformedImport;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsCorrect1.yang");
            InterpreterInproperYangVersion = YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsInproperYangVer.yang");
            InterpreterMissingSemicolonInValue = YangInterpreterTool.Load("TestFiles/ModuleTests/InterpreterMissingSemicolonInValue.yang");
            InterpreterInproperMalformedImport = YangInterpreterTool.Load("TestFiles/ModuleTests/InterpreterInproperMalformedImport.yang");
        }

        /// <summary>
        /// Checks if the module name is parsed correctly from the yang file.
        /// </summary>
        [Test]
        public void ModuleNameParsedCorrectlyTest()
        {
            Assert.AreEqual("CorrectTestModule1", InterpreterCorrect.Root.Name);
        }

        /// <summary>
        /// Checks if the yangparserversion got parsed correctly from the file.
        /// </summary>
        [Test]
        public void YangVersionParsedCorrectly()
        {
            var Nodes = InterpreterCorrect.Root.DescendantsNode("yang-version");
            Assert.IsTrue(Nodes.Count() > 0);
            var YangVersionNode = Nodes.First() as YangVersionNode;
            Assert.AreEqual("1", YangVersionNode.Value);
        }

        /// <summary>
        /// Checks if the Namespace was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleNameSpaceParsedCorrectlyTest()
        {
            Assert.AreEqual("NamespaceCorrectTestModule1", InterpreterCorrect.Root.Namespace);
        }

        /// <summary>
        /// Checks if the prefix was parsed correctly.
        /// </summary>
        [Test]
        public void ModulePrefixParsedCorrectlyTest()
        {
            Assert.AreEqual("nctm", InterpreterCorrect.Root.Prefix);
        }

        /// <summary>
        /// Checks if module imperts was successfuly parsed.
        /// </summary>
        [Test]
        public void ModuleImportWithPrefixParsedCorrectlyTest()
        {
            Assert.IsTrue(InterpreterCorrect.Root.NamespaceDictionary.Count == 3);
            Assert.AreEqual("yang-interpreter", InterpreterCorrect.Root.NamespaceDictionary["yani"]);
            Assert.AreEqual("interpreter-files", InterpreterCorrect.Root.NamespaceDictionary["in-fi"]);
            Assert.AreEqual("profiles", InterpreterCorrect.Root.NamespaceDictionary["profiles"]);
        }

        /// <summary>
        /// Checks if the organization was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleOrganizationParsedCorrectlyTest()
        {
            Assert.AreEqual("My diplomwork corp", InterpreterCorrect.Root.Organization);
        }

        /// <summary>
        /// Checks if the contact was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleContactParsedCorrectlyTest()
        {
            Assert.AreEqual("Adam Sranko srankoadam@gmail.com", InterpreterCorrect.Root.Contact);
        }

        /// <summary>
        /// Checks if the description was parsed correctly.
        /// </summary>
        [Test]
        public void ModuleDescriptionParsedCorrectlyTest()
        {
            Assert.AreEqual("Description of correctly formatted\r\n\t\tmodule,\r\nwith multiline value.", InterpreterCorrect.Root.GetPropertyByName("description").Single().GetValue());
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////INPROPER FORMATION TESTS/////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///


        [Test]
        public void ModulBadYangversionException()
        {
            Assert.Throws<InvalidYangVersion>(() => YangInterpreterTool.Load("TestFiles/ModuleTests/ModuleStatementsInproperYangVer.yang"));
        }
    }
}