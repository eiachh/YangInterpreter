using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace InterpreterNUnitTester
{
    public class RpcStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/RpcStatement/RpcStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the rpc statement is parsed correctly.
        /// </summary>
        [Test]
        public void RpcIsParsedCorrectly()
        {
            var rpc = InterpreterCorrect.Root.Descendants("rpc").Single(statement => statement.Value == "mainTester");
            Assert.AreEqual(8, rpc.Elements().Count());
        }

        /// <summary>
        /// Checks if the rpc statement throws error at whitelisted element overflow.
        /// </summary>
        [Test]
        public void RpcStatementWhitelistOverflowError()
        {
            var rpc = InterpreterCorrect.Root.Descendants("rpc").Single(statement => statement.Value == "mainTester");
            Assert.Throws<ArgumentOutOfRangeException>(() => rpc.AddStatement(new BooleanTypeStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => rpc.AddStatement(new DescriptionStatement("desc")));
            Assert.Throws<ArgumentOutOfRangeException>(() => rpc.AddStatement(new InputStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => rpc.AddStatement(new OutputStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => rpc.AddStatement(new ReferenceStatement()));
            Assert.Throws<ArgumentOutOfRangeException>(() => rpc.AddStatement(new StatusStatement()));
        }
    }
}