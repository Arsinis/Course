using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace Guard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Manager manager = new Manager();
            manager.Run();
            Console.ReadKey();
        }
    }
}
