using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements.BaseStatements;

namespace InterpreterNUnitTester
{
    public class NamespaceStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/NamespaceStatement/NamespaceStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the namespace statement is parsed correctly.
        /// </summary>
        [Test]
        public void NamespaceIsParsedCorrectly()
        {
            var nameSp = InterpreterCorrect.Root.Descendants("namespace").Single();
            Assert.AreEqual("selfNamespace", nameSp.Value);
        }

        /// <summary>
        /// Checks if the namespace statement is childless.
        /// </summary>
        [Test]
        public void NamespaceStatementIsChildless()
        {
            var nameSp = InterpreterCorrect.Root.Descendants("namespace").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => nameSp.AddStatement(new EmptyLineStatement()));
        }
    }
}