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
    public class ImportStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/ImportStatement/ImportStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the import statement is parsed correctly.
        /// </summary>
        [Test]
        public void ImportIsParsedCorrectly()
        {
            var import = InterpreterCorrect.Root.Descendants("import").Single();
            Assert.AreEqual(2, import.Elements().Count());
        }

        /// <summary>
        /// Checks if the import statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void ImportStatementWhitelistOverflowError()
        {
            var import = InterpreterCorrect.Root.Descendants("import").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => import.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => import.AddStatement(new PrefixStatement("value")));
            Assert.Throws<ArgumentOutOfRangeException>(() => import.AddStatement(new RevisionDateStatement("3232.12.3")));
        }

        /// <summary>
        /// Checks if the prefix statement is registering in the module.
        /// </summary>
        [Test]
        public void ImportRootRegistrationTest()
        {
            Assert.AreEqual("mainTester", InterpreterCorrect.Root.NamespaceDictionary["m-t"]);
        }

        /// <summary>
        /// Checks if the import statement is refreshing in the module after change.
        /// </summary>
        [Test]
        public void importRootRegistrationRefreshOnChange()
        {
            var import = InterpreterCorrect.Root.Descendants("import").Single();
            Assert.AreEqual("mainTester", InterpreterCorrect.Root.NamespaceDictionary["m-t"]);
            import.Value = "newVal";
            Assert.AreEqual("newVal", InterpreterCorrect.Root.NamespaceDictionary["m-t"]);
        }
    }
}