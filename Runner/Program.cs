using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YangInterpreter;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;
using YangInterpreter.Statements.BaseStatements;

namespace Runner
{
    class Program
    {
        static string testfolder = @"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\YangInterpreter\InterpreterNUnitTester\TestFiles";
        static void Main(string[] args)
        {
            //typeof(NamespaceStatement).type
            var asdds = typeof(NamespaceStatement).IsAssignableFrom(typeof(StatementWithSingleValueBase));
            var weweasdds = typeof(StatementWithSingleValueBase).IsAssignableFrom(typeof(NamespaceStatement));
            var asddsds = new Regex("^(?:[0-9]+\\.\\.[0-9]+)(?:\\s?\\|\\s?(?:[0-9]*\\.\\.[0-9]*))*$");
            var math = asddsds.Match("2323..323d23");


            BitsTypeStatement a = new BitsTypeStatement();
            var sadsd = a.GetType().BaseType;
            var sadsd2 = a.GetType().BaseType.BaseType;
            var sadsd3 = a.GetType().BaseType.BaseType.BaseType;


            string asd = "asdasdda \r\n and \n";
            var counted = asd.Count(x => x == '\n');

            YangInterpreterTool tool = YangInterpreterTool.Load((Path.Combine(testfolder, "Bit\\BitTypeCorrect.yang")));

            if (!Directory.Exists(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles"))
                Directory.CreateDirectory(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\");
            StreamWriter w = new StreamWriter(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\rewrite.yang");
            w.WriteLine(tool.Root.StatementAsYangString());
            w.Close();

        }
    }
}
