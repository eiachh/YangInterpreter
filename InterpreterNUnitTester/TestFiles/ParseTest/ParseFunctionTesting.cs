using NUnit.Framework;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Interpreter;

namespace InterpreterNUnitTester
{
    public class ParseFunctionTesting
    {
        YangInterpreterTool InterpreterCorrect;
        YangInterpreterTool InterpreterForce;
        [SetUp]
        public void Setup()
        {
            InterpreterCorrect = YangInterpreterTool.Parse("module testModul {\r\n leaf testLeaf {\r\n type int8; } }");
            InterpreterForce = YangInterpreterTool.Parse("module testModul {\r\n yang-version 1.1;\r\n leaf testLeaf {\r\n type int8; } }",InterpreterOption.Force);
        }

        /// <summary>
        /// Checks parsing directly from text.
        /// </summary>
        [Test]
        public void ParseFunctionWithProperYangVer()
        {
            Assert.AreEqual("testLeaf", InterpreterCorrect.Root.Descendants("leaf").Single().Argument);
            Assert.AreEqual(3, InterpreterCorrect.Root.Descendants().Count());
        }

        /// <summary>
        /// Checks parsing directly from text with bad yangversion.
        /// </summary>
        [Test]
        public void ParseFunctionWithWrongYangVer()
        {
            Assert.Throws<InvalidYangVersion>(() => YangInterpreterTool.Parse("module testModul {\r\n yang-version 1.1;\r\n leaf testLeaf {\r\n type int8; } }"));
        }

        /// <summary>
        /// Checks parsing directly from text with force option.
        /// </summary>
        [Test]
        public void ParseFunctionWithForce()
        {
            Assert.AreEqual("testLeaf", InterpreterForce.Root.Descendants("leaf").Single().Argument);
            Assert.AreEqual(3, InterpreterForce.Root.Descendants().Count());
        }
    }
}