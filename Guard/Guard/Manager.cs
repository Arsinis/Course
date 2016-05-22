using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guard
{
    public class Manager
    {
        public List<String> Tokens;
        public string Error = "Oops, something go wrong";
        public string Attack;
        public int time = 4100;
        public void Run()
        {
            string path = "e:\\base.txt";
            FIleWorker fw = new FIleWorker();
            Base.Signatures = fw.Read(path);
            Console.WriteLine("System loaded. Type 'help' for more information");
            string command = null;
            do
            {
                Console.Write(">");
                command = Console.ReadLine();
                switch (command)
                {
                    case "help":
                    {
                        Commander comm = new Commander();
                        comm.LoadCommands();
                        foreach (var b in comm.commands)
                        {
                            Console.WriteLine(b.Name + "    " + b.Description);
                        }
                    }
                    break;
                    case "cls":
                    {
                        Console.Clear();
                    }
                    break;
                    case "set":
                    {
                        Console.Write("New value:");
                        time = Int32.Parse(Console.ReadLine());
                    }
                    break;
                    case "scan -t -q":
                    {
                        StreamReader sr = new StreamReader("e:\\1.txt");
                        Attack = sr.ReadLine();
                        sr.Close();
                        Analyzers sc = new Analyzers();
                        sc.QuickScan(Base.Signatures, Attack);
                    }
                    break;
                    case "scan -t -d":
                    {
                        StreamReader sr = new StreamReader("e:\\1.txt");
                        Attack = sr.ReadLine();
                        sr.Close();
                        Analyzers sc = new Analyzers();
                        sc.DeepScan(Base.Signatures, Attack);
                    }
                    break;
                    case "scan -r -q":
                    {
                        Scanners sc = new Scanners();
                        sc.Scan(time);
                        sc.GenerateAttack();
                        StreamReader sr = new StreamReader("e:\\1.txt");
                        Attack = sr.ReadLine();
                        sr.Close();
                        Analyzers an = new Analyzers();
                        an.QuickScan(Base.Signatures, Attack);
                    }
                    break;
                    case "scan -r -d":
                    {
                        Scanners sc = new Scanners();
                        sc.Scan(time);
                        sc.GenerateAttack();
                        StreamReader sr = new StreamReader("e:\\1.txt");
                        Attack = sr.ReadLine();
                        sr.Close();
                        Analyzers an = new Analyzers();
                        an.DeepScan(Base.Signatures, Attack);
                    }
                    break;
                    case "exit":
                    {
                        Console.WriteLine("System deactivated");   
                        Console.Beep();
                    }
                    break;
                    default:
                    {
                       Console.WriteLine("Incorrect command. Try 'help' for list of commands"); 
                    }
                    break;
                }
            }
            while (command!="exit");          
        }

    }
}
