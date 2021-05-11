using System.IO;
using YangInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using YangInterpreter.Statements.BaseStatements;
using System.Xml.Linq;
using System.Xml;
using System.Text;

namespace Demo
{
    enum ProgramState
    {
        main,
        moduleInfo,
        elements,
        descendants

    }
    class Program
    {
        static void Main(string[] args)
        {
            ProgramState state = ProgramState.main;
            YangInterpreterTool tool = YangInterpreterTool.Load("DemoModule.yang");

            var input = "";
            
            while(input != "quit")
            {
                if (state == ProgramState.main)
                {
                    PrintMainMenu();
                    input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.Clear();
                        state = ProgramState.moduleInfo;
                        
                    }
                    else if (input == "2")
                    {
                        HandleChildSearchSearch(tool, true);
                    }
                    else if (input == "3")
                    {
                        HandleChildSearchSearch(tool, false);
                    }
                    else if (input == "4")
                    {
                        Console.WriteLine(tool.Root.ToString());
                        Console.WriteLine("A továbblépéshez nyomjon entert...");
                        Console.ReadLine();
                    }
                    else if (input == "5")
                    {
                        Console.WriteLine(ToStringFormatted(tool.Root.StatementAsXML().FirstOrDefault()));
                        StreamWriter w = new StreamWriter("DemoOutput.xml");
                        tool.Root.StatementAsXML().FirstOrDefault().Save(w,SaveOptions.DisableFormatting);
                        Console.WriteLine("A továbblépéshez nyomjon entert...");
                        Console.ReadLine();
                    }
                }
                else if(state == ProgramState.moduleInfo){
                    PrintModuleOptions();
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":                           
                            ShowSeparatedData("A betöltött modul neve(argumentumként): " + tool.Root.Argument);
                            break;
                        case "2":
                            var yangVersionStatement = tool.Root.Descendants("yang-version").Single();
                            ShowSeparatedData("Yang verziója: " + yangVersionStatement.Argument);
                            break;
                        case "3":
                            ShowSeparatedData("A betöltött modul névtere: " + tool.Root.Namespace);
                            break;
                        case "4":
                            ShowSeparatedData("A betöltött modul prefixe: " + tool.Root.Prefix);
                            break;
                        case "5":
                            Console.WriteLine("----------------------------------------------------");
                            foreach (var item in tool.Root.NamespaceDictionary)
                            {
                                Console.WriteLine("Prefix: "+item.Key+"| Teljes névtér: "+item.Value);   
                            }
                            Console.WriteLine("----------------------------------------------------");
                            break;
                        case "6":
                            Console.Clear();
                            state = ProgramState.main;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("1: Modul közvetlen adataival kapcsolatos műveletek.");
            Console.WriteLine("2: Modul közvetlen gyermekének választása név megadásával.");
            Console.WriteLine("3: Modul bármely gyermekének választása név megadásával.");
            Console.WriteLine("4: Modul szöveges megjelenítése.");
            Console.WriteLine("5: Modul xml formátumú megjelenítése");
            Console.WriteLine("quit: Kilépés.");
        }

        static void PrintModuleOptions()
        {
            Console.WriteLine("1: Modul neve.");
            Console.WriteLine("2: Modul yang verziója.");
            Console.WriteLine("3: Modul névtere.");
            Console.WriteLine("4: Modul prefixe.");
            Console.WriteLine("5: Modul és az általa importált névterek listája.");
            Console.WriteLine("6: Vissza a főmenübe.");
            Console.WriteLine("quit: Kilépés.");
        }

        static string GetName()
        {
            Console.Clear();
            Console.WriteLine("Adja meg a kívánt gyermek nevét: ");
            return Console.ReadLine();
        }
        static string GetArgument()
        {
            Console.WriteLine("Adja meg a kívánt gyermek argumentunát: ");
            return Console.ReadLine();
        }
        static void ShowSeparatedData(string text)
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(text);
            Console.WriteLine("----------------------------------------------------");
        }
        static void HandleChildSearchSearch(YangInterpreterTool tool, bool IsElements)
        {
            StatementBase selectedStatement = null;
            var selectedName = GetName();
            IEnumerable<StatementBase> elements;
            if(IsElements)
                elements = tool.Root.Elements(selectedName);
            else
                elements = tool.Root.Descendants(selectedName);
            if (elements?.Count() > 1)
            {
                Console.WriteLine("Több statement is létezik ami tartalmazza a megadott nevet: " + selectedName);
                foreach (var item in elements)
                {
                    Console.WriteLine("Statement neve: " + item.Name + " | Statement argumentuma: " + item.Argument);
                }
                var selectedArg = GetArgument();
                selectedStatement = elements.FirstOrDefault(statement => statement.Argument == selectedArg);
            }
            else
                selectedStatement = elements?.FirstOrDefault();
            if (selectedStatement != null)
                ShowSeparatedData("Kiválasztott elem neve: " + selectedStatement.Name + " Kiválasztott statement argumentuma: " + selectedStatement.Argument+
                    Environment.NewLine + selectedStatement.ToString());
            Console.WriteLine("A továbblépéshez nyomjon entert...");
            Console.ReadLine();

        }

        static string ToStringFormatted(XElement xml)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            StringBuilder result = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(result, settings))
            {
                xml.WriteTo(writer);
            }
            return result.ToString();
        }
    }
}
