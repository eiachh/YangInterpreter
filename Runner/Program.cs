using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;

namespace Runner
{
    class Program
    {
        static string testfolder = @"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\YangInterpreter\InterpreterNUnitTester\TestFiles\ModuleTests";
        static void Main(string[] args)
        {

            string asd = "asdasdda \r\n and \n";
            var counted = asd.Count(x => x == '\n');

            YangInterpreterTool tool = YangInterpreterTool.Load((Path.Combine(testfolder, "Organization\\OrganizationCorrectNextLineStart.yang")));

            if (!Directory.Exists(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles"))
                Directory.CreateDirectory(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\");
            StreamWriter w = new StreamWriter(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\rewrite.yang");
            w.WriteLine(tool.Root.StatementAsYangString());
            w.Close();

        }
    }
}
