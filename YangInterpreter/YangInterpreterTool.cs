using System.IO;

namespace YangInterpreter
{
    public enum InterpreterOption
    {
        Force,
        Normal
    }

    /// <summary>
    /// Used to parse Yang compatible text or files into runtime objects. Only checks if a given data is syntactically correct does NOT check semantics.
    /// </summary>
    public class YangInterpreterTool
    {
        public ModuleStatement Root { get; set; }

        /// <summary>
        /// Parses the yang file from the given text.
        /// </summary>
        /// <param name="YangAsRawText">Text as which is equal to a valid yang file.</param>
        /// <returns>Returns a YangInterpreterTool object with the loaded yang file.</returns>
        public static YangInterpreterTool Parse(string YangAsRawText, InterpreterOption opt = InterpreterOption.Normal)
        {         
            return new YangInterpreterTool(YangAsRawText, false, opt);
        }

        /// <summary>
        /// Loads the yang file from file.
        /// </summary>
        /// <param name="Path">Path to the yang file.</param>
        /// <returns>Returns a YangInterpreterTool object with the loaded yang file.</returns>
        public static YangInterpreterTool Load(string Path,InterpreterOption opt = InterpreterOption.Normal)
        {
            return new YangInterpreterTool(Path, true, opt);
        }

        /// <summary>
        /// Runs the interpreter on the given string and sets the root variable of this class as expected
        /// </summary>
        /// <param name="InputStr"></param>
        /// <param name="IsPath"></param>
        private YangInterpreterTool(string InputStr, bool IsPath, InterpreterOption opt)
        {
            string YangAsRawText;

            if (!IsPath)
            {
                YangAsRawText = InputStr;
            }
            else
            {
                YangAsRawText = File.ReadAllText(InputStr);
            }
            Interpreter.Interpreter interpreter = new Interpreter.Interpreter(opt);
            Root = interpreter.ConvertText(YangAsRawText) as ModuleStatement;
        }
    }
}
