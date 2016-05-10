using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace Guard
{
    public class Program
    {
        public static List<String> data, tokens;
        public static String error = "Oops, something go wrong";
        public static void Main(string[] args)
        {
            RegistryKey hklm = Registry.LocalMachine;
            String path = "e:\\base.txt", attack;
            FIleWorker fw = new FIleWorker();
            data = fw.Read(path);
            Console.WriteLine("Type of scan");
            Console.WriteLine("1.Real-time");
            Console.WriteLine("2.Training");
            int c = Int32.Parse(Console.ReadLine());
            switch(c)
            {
                case 1:
                    {
                        Scanners sc = new Scanners();
                        sc.Scan(hklm);
                    }
                    break;
                case 2:
                {
                    StreamReader sr = new StreamReader("e:\\1.txt");
                    attack = sr.ReadLine();
                    sr.Close();
                    Console.WriteLine("How to proceed?");
                    Console.WriteLine("1.Quick scan");
                    Console.WriteLine("2.Deep scan");
                    int nc = Int32.Parse(Console.ReadLine());
                    switch (nc)
                    {
                        case 1:
                        {
                            Analyzers sc = new Analyzers();
                            sc.QuickScan(data, attack);
                        }
                        break;
                        case 2:
                        {
                            Analyzers sc = new Analyzers();
                            sc.DeepScan(data, attack);
                        }
                        break;
                    }
                }
                break;
            }
            Console.ReadKey();
        }
    }
}
