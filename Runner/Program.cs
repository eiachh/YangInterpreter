using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace Runner
{
    class Program
    {
        static string testfolder = @"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\YangInterpreter\InterpreterNUnitTester\TestFiles\ModuleTests";
        static void Main(string[] args)
        {
            BitsTypeStatement a = new BitsTypeStatement();
            var sadsd = a.GetType().BaseType;
            var sadsd2 = a.GetType().BaseType.BaseType;
            var sadsd3 = a.GetType().BaseType.BaseType.BaseType;


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
