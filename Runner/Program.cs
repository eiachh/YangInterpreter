using System.IO;
using YangInterpreter;

namespace Runner
{
    class Program
    {
        static string testfolder = @"C:\Users\Eiachh\Desktop\suli\szakdogaRework\YangInterpreter\InterpreterNUnitTester\TestFiles\";
        static void Main(string[] args)
        {
            YangInterpreterTool tool = YangInterpreterTool.Load((Path.Combine(testfolder, "Bit\\BitTypeCorrect.yang")));

            if (!Directory.Exists(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles"))
                Directory.CreateDirectory(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\");
            StreamWriter w = new StreamWriter(@"C:\Users\sranko\Desktop\definitlyWorkReleated\szakdogarework\testfiles\rewrite.yang");
            w.WriteLine(tool.Root.ToString());
            w.Close();

        }
    }
}
