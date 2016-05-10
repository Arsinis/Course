using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using 

namespace Guard
{
    public class Scanners
    {
        public void Scan(RegistryKey hklm)
        {
            string attack = "";
            RegistryKey hklmnow = Registry.LocalMachine;
            if (hklmnow == hklm)
                attack += "workwithregistry=no and "; 
            else
                attack += "workwithregistry=yes and ";


            StreamWriter sw = new StreamWriter(@"e:\\1.txt");
            sw.Write(attack);
            sw.Close();
        }
    }
}
