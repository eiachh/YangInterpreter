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
    public class IntegerTypesTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/IntegerTypes/IntegerTypesCorrect.yang");
        }

        /// <summary>
        /// Checks if the Int8 type is parsed correctly.
        /// </summary>
        [Test]
        public void Int8ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "intTest1").Single().Elements().First();
            Assert.AreEqual("int8", typeStatement.Value);
            Assert.AreEqual(1, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the Int16 type is parsed correctly.
        /// </summary>
        [Test]
        public void Int16ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "intTest2").Single().Elements().First();
            Assert.AreEqual("int16", typeStatement.Value);
            Assert.AreEqual(0, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the Int32 type is parsed correctly.
        /// </summary>
        [Test]
        public void Int32ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "intTest3").Single().Elements().First();
            Assert.AreEqual("int32", typeStatement.Value);
            Assert.AreEqual(1, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the Int64 type is parsed correctly.
        /// </summary>
        [Test]
        public void Int64ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "intTest4").Single().Elements().First();
            Assert.AreEqual("int64", typeStatement.Value);
            Assert.AreEqual(0, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the UInt8 type is parsed correctly.
        /// </summary>
        [Test]
        public void UInt8ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "uintTest1").Single().Elements().First();
            Assert.AreEqual("uint8", typeStatement.Value);
            Assert.AreEqual(1, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the UInt16 type is parsed correctly.
        /// </summary>
        [Test]
        public void UInt16ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "uintTest2").Single().Elements().First();
            Assert.AreEqual("uint16", typeStatement.Value);
            Assert.AreEqual(0, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the UInt32 type is parsed correctly.
        /// </summary>
        [Test]
        public void UInt32ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "uintTest3").Single().Elements().First();
            Assert.AreEqual("uint32", typeStatement.Value);
            Assert.AreEqual(1, typeStatement.Elements().Count());
        }

        /// <summary>
        /// Checks if the UInt64 type is parsed correctly.
        /// </summary>
        [Test]
        public void UInt64ParsedCorrectly()
        {
            var typeStatement = InterpreterCorrect.Root.Descendants("leaf").Where(leaf => leaf.Value == "uintTest4").Single().Elements().First();
            Assert.AreEqual("uint64", typeStatement.Value);
            Assert.AreEqual(0, typeStatement.Elements().Count());
        }
    }
}