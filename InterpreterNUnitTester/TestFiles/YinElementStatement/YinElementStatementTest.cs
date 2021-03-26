using NUnit.Framework;
using System;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;

namespace InterpreterNUnitTester
{
    public class YinElementStatementTest
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/YinElementStatement/YinElementStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the yin-element statement is parsed correctly.
        /// </summary>
        [Test]
        public void YinElementIsParsedCorrectly()
        {
            var yinElement = InterpreterCorrect.Root.Descendants("yin-element").Single();
            Assert.AreEqual("true", yinElement.Value);
        }

        /// <summary>
        /// Checks if the yin-element statement is childless.
        /// </summary>
        [Test]
        public void YinElementIsChildless()
        {
            var yinElement = InterpreterCorrect.Root.Descendants("yin-element").Single();
            Assert.Throws<ArgumentOutOfRangeException>(() => yinElement.AddStatement(new EmptyLineStatement()));
        }

        /// <summary>
        /// Checks if the yin-element statement is only accepting true or false.
        /// </summary>
        [Test]
        public void YinElementControlledValueCheck()
        {
            var yinElement1 = new YinElementStatement("true");
            var yinElement2 = new YinElementStatement("false");
            
            Assert.AreEqual("true", yinElement1.Value);
            Assert.AreEqual("false", yinElement2.Value);

            Assert.Throws<ImproperValue>(() => new YinElementStatement("error"));
            Assert.Throws<ImproperValue>(() => yinElement1.Value = "error");
        }
    }
}