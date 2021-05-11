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
    public class StatusStatementTester
    {
        YangInterpreterTool InterpreterCorrect;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Load("TestFiles/StatusStatement/StatusStatementCorrect.yang");
        }

        /// <summary>
        /// Checks if the status statement is parsed correctly.
        /// </summary>
        [Test]
        public void StatusValuesParsedCorrectly()
        {
            var BitStatements = InterpreterCorrect.Root.Descendants("bit");
            var Bit1 = BitStatements.Where(x => x.Parent.Parent.Argument == "mybits1").Single();

            var BitStatements2 = InterpreterCorrect.Root.Descendants("bit");
            var Bit2 = BitStatements.Where(x => x.Parent.Parent.Argument == "mybits2").Single();

            var BitStatements3 = InterpreterCorrect.Root.Descendants("bit");
            var Bit3 = BitStatements.Where(x => x.Parent.Parent.Argument == "mybits3").Single();

            Assert.AreEqual("current", Bit1.Descendants("status").First().Argument);
            Assert.AreEqual("obsolete", Bit2.Descendants("status").First().Argument);
            Assert.AreEqual("deprecated", Bit3.Descendants("status").First().Argument);
        }

        /// <summary>
        /// Has to throw ImproperValue if wrong value is given for status.
        /// </summary>
        [Test]
        public void InvalidValueThrowsError()
        {
            Assert.Throws<ImproperValue>(() => new StatusStatement("notValid"));
            Assert.Throws<ImproperValue>(() => YangInterpreterTool.Load("TestFiles/StatusStatement/StatusStatementInvalidValue.yang"));
        }

        /// <summary>
        /// Has to throw ImproperValue if wrong vale is given for status constructor.
        /// </summary>
        [Test]
        public void InvalidconstructorValueThrowsError()
        {
            Assert.Throws<ImproperValue>(() => new StatusStatement("notValid"));
        }

        /// <summary>
        /// Has to throw ImproperValue when value is changed to an invalid string.
        /// </summary>
        [Test]
        public void InvalidValueChangeThrowsError()
        {
            StatusStatement stat = new StatusStatement("current");
            Assert.Throws<ImproperValue>(() => stat.Argument = "notValid");
        }
    }
}