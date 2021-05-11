using NUnit.Framework;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Interpreter;
using System;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class BinaryTypeStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Binary/BinaryTypeCorrect.yang");
        }

        /// <summary>
        /// Checks if the binary type statement is parsed correctly.
        /// </summary>
        [Test]
        public void BinaryWithLengthRestrictionParsedCorrectly()
        {
            var binaryTestLeaf = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "binaryTest1").Single();
            var binaryType = binaryTestLeaf.Elements().First();
            Assert.AreEqual(1, binaryType.Elements().Count());
            var elementOfBinary = binaryType.Elements().First();
            Assert.AreEqual("5..78", elementOfBinary.Argument);
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Checks if the binary type statement is parsed correctly.
        /// </summary>
        [Test]
        public void BinaryWithoutLengthRestrictionParsedCorrectly()
        {
            var binaryTestLeaf = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Argument == "binaryTest2").Single();
            var binaryType = binaryTestLeaf.Elements().First();
            Assert.AreEqual(0, binaryType.Elements().Count());
            Assert.AreEqual("type binary;", binaryType.ToString());
            //Assert.AreEqual("2019-09-11", InterpreterCorrect.Root.DescendantsNode("revision").Single().Value);
            //Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/Revision/RevisionStatementImproperValue.yang"));
        }

        /// <summary>
        /// Binary type has to throw exception if more than 1 length statement is added to it.
        /// </summary>
        [Test]
        public void BinaryTypeOverflowExceptionThrow()
        {
            var binary = new BinaryTypeStatement();
            binary.AddStatement(new LengthStatement("3..4"));
            Assert.Throws<ArgumentOutOfRangeException>(() => binary.AddStatement(new LengthStatement("3..4")));
        }
    }
}