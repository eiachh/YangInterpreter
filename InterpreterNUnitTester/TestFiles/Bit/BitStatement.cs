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
    public class BitStatement
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/Bit/BitTypeCorrect.yang");
        }

        /// <summary>
        /// Checks if the Type Bits value is parsed correctly.
        /// </summary>
        [Test]
        public void BitStatementParsedCorrectly()
        {
            var BitStatements = InterpreterCorrect.Root.Descendants("bit");
            var Bit = BitStatements.Where(x => x.Parent.Parent.Value == "mybits1").Single();
            Assert.AreEqual("disable-nagle", Bit.Value);
            Assert.AreEqual(4, Bit.Count());
            Assert.AreEqual("0", Bit.Descendants("position").Single().Value);
            Assert.AreEqual("Description of\r\nbit.", Bit.Descendants("description").Single().Value);
            Assert.AreEqual("Reference of bit.", Bit.Descendants("reference").Single().Value);
        }

        /// <summary>
        /// Bit throws ArgumentOutOfRangeException if more than 1 position is given.
        /// </summary>
        [Test]
        public void BitStatementArgumentOutOfRangeTestPosition()
        {
            var BitStatements = InterpreterCorrect.Root.Descendants("bit");
            var Bit = BitStatements.Where(x => x.Parent.Parent.Value == "mybits1").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => Bit.AddStatement(new Position("0")));
        }

        /// <summary>
        /// Bit throws ArgumentOutOfRangeException if more than 1 description is given.
        /// </summary>
        [Test]
        public void BitStatementArgumentOutOfRangeTestDescription()
        {
            var BitStatements = InterpreterCorrect.Root.Descendants("bit");
            var Bit = BitStatements.Where(x => x.Parent.Parent.Value == "mybits1").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => Bit.AddStatement(new YangInterpreter.Statements.DescriptionStatement("some desc")));
        }

        /// <summary>
        /// Bit throws ArgumentOutOfRangeException if more than 1 reference is given.
        /// </summary>
        [Test]
        public void BitStatementArgumentOutOfRangeTestReference()
        {
            var BitStatements = InterpreterCorrect.Root.Descendants("bit");
            var Bit = BitStatements.Where(x => x.Parent.Parent.Value == "mybits1").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => Bit.AddStatement(new ReferenceStatement("some ref")));
        }

        /// <summary>
        /// Bit throws ArgumentOutOfRangeException if more than 1 status is given.
        /// </summary>
        [Test]
        public void BitStatementArgumentOutOfRangeTestStatus()
        {
            var BitStatements = InterpreterCorrect.Root.Descendants("bit");
            var Bit = BitStatements.Where(x => x.Parent.Parent.Value == "mybits1").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => Bit.AddStatement(new YangInterpreter.Statements.StatusStatement("current")));
        }
    }
}