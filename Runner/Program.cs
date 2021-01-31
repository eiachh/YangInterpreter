using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YangInterpreter;

namespace Runner
{
    class Program
    {
        static string testfolder = @"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles";
        static void Main(string[] args)
        {
            string asd = "asdasdda \r\n and \n";
            var counted = asd.Count(x => x == '\n');

            YangInterpreterTool tool = YangInterpreterTool.Load(Path.Combine(testfolder, "simple1.yang"),InterpreterOption.Force);

            StreamWriter w = new StreamWriter(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\rewrite.yang");
            w.WriteLine(tool.Root.NodeAsYangString());
            w.Close();

        }
    }
}
