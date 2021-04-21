using System;
using System.Collections.Generic;
using System.IO;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Interpreter
{
    /// <summary>
    /// Used to set LoggingOptions on ErrorLogger
    /// </summary>
    public enum LoggingOptions
    {
        Console,
        TextLog,
    }

    /// <summary>
    /// Class that used to output errors easily. Set LoggingOprions to TextLog for txt save (LoggingPath has to be set).
    /// </summary>
    public class ErrorLogger
    {
        public int RowNumber { get; set; }
        public string Row { get; set; }
        public LoggingOptions LoggingOption { get; set; }

        /// <summary>
        /// Filepath to Logging Location with file at end. Default location AssemblyDirPath/YangInterpreterLogFiles/ErrorLog.txt
        /// </summary>
        public string LoggingPath { get; set; }

        public ErrorLogger(int rowNumber, string row, LoggingOptions loggingOption = LoggingOptions.TextLog) 
        {
            Row = row;
            RowNumber = rowNumber;
            LoggingOption = loggingOption;

            var AssemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var AssemblyDirPath = Directory.GetParent(AssemblyLocation).FullName;

            LoggingPath = Path.Combine(AssemblyDirPath, "YangInterpreterLogFiles/ErrorLog.txt");
        }

        /// <summary>
        /// Returns null if could not create the log file.
        /// </summary>
        /// <param name="StatementWithParseError"></param>
        /// <returns></returns>
        public bool CreateLog(StatementBase StatementWithParseError, Stack<Type> StatusStack, Token ParsedToken)
        {
            try
            {
                string LogText = BuildOutputString(StatementWithParseError, StatusStack, ParsedToken);
                return LogOnExpectedOutput(LogText);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates log in the desired location. Returns false if failed.
        /// </summary>
        /// <param name="StatementWithParseError"></param>
        /// <param name="StatusStack"></param>
        /// <returns></returns>
        public bool CreateLog(StatementBase StatementWithParseError, Stack<Type> StatusStack)
        {
            try
            {
                string LogText = BuildOutputString(StatementWithParseError, StatusStack);
                return LogOnExpectedOutput(LogText);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StatusStack"></param>
        /// <returns></returns>
        private string MakeStatusStackIntoString(Stack<Type> StatusStack)
        {
            string strBuilder = string.Empty;
            Stack<Type> ReversedStatusStack = new Stack<Type>(StatusStack);

            foreach (var status in ReversedStatusStack)
            {
                if(status.Name != "start")
                    strBuilder += status.Name + "-->";
            }
            return strBuilder;
        }

        /// <summary>
        /// Builds the output string from the given error, token and status stack.
        /// </summary>
        /// <param name="StatementWithParseError"></param>
        /// <param name="StatusStack"></param>
        /// <param name="ParsedToken"></param>
        /// <returns></returns>
        private string BuildOutputString(StatementBase StatementWithParseError, Stack<Type> StatusStack, Token ParsedToken) 
        {
            string strBuilder = string.Empty;
            strBuilder += "Error at row: " + RowNumber + Environment.NewLine;
            strBuilder += "Row content:-->" + Row+ "<--" + Environment.NewLine;
            strBuilder += "The interpreter parsed the previously mentioned line as:-->" + ParsedToken.TokenType+ "<--" + Environment.NewLine;
            strBuilder += "The last yang statement was parsed as: " + Environment.NewLine;
            strBuilder += StatementWithParseError.StatementAsYangString() + Environment.NewLine;
            strBuilder += "Stack trace: " + MakeStatusStackIntoString(StatusStack);
            return strBuilder;
        }

        /// <summary>
        /// Builds the output string from the given error and status stack if the token creation wasnt successful.
        /// </summary>
        /// <param name="StatementWithParseError"></param>
        /// <param name="StatusStack"></param>
        /// <returns></returns>
        private string BuildOutputString(StatementBase StatementWithParseError, Stack<Type> StatusStack)
        {
            string strBuilder = string.Empty;
            strBuilder += "Error at row: " + RowNumber + Environment.NewLine;
            strBuilder += "Row content: " + Row + Environment.NewLine;
            strBuilder += "The interpreter could not parse the mentioned row." + Environment.NewLine;
            strBuilder += "The last yang statement was parsed as: " + Environment.NewLine;
            strBuilder += StatementWithParseError.StatementAsYangString() + Environment.NewLine;
            strBuilder += "Stack trace: " + MakeStatusStackIntoString(StatusStack);
            return strBuilder;
        }

        /// <summary>
        /// Picks the output and shows log on it.
        /// </summary>
        /// <param name="LogText"></param>
        /// <returns></returns>
        private bool LogOnExpectedOutput(string LogText)
        {
            if(LoggingOption == LoggingOptions.TextLog)
            {
                if (string.IsNullOrEmpty(LoggingPath))
                    return false;
                var LoggingDirPath = Path.GetDirectoryName(LoggingPath);
                if (!Directory.Exists(LoggingDirPath))
                    Directory.CreateDirectory(LoggingDirPath);
                StreamWriter w = new StreamWriter(LoggingPath);
                w.Write(LogText);
                w.Close();
                return true;
            }
            else
            {
                Console.WriteLine(LogText);
                return true;
            }
        }
    }
}
