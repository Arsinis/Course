using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guard
{
    public class Analyzers
    {
        public List<string> tokens = new List<string>();
        public void QuickScan(List<string> data, string attack)
        {
            bool found = false;
            foreach (var str in data)
            {
                if (str.Contains(attack) && str.Contains("not"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("It's allright. Nothning bad");
                    Console.ResetColor();
                    found = true;
                    break;
                }
                if (str.Contains(attack) && !str.Contains("not"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning!!! Network attack detected!");
                    Console.ResetColor();
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Can't say anymore... Try to analyze more deeply");
                Console.ResetColor();
            }
        } 
        public void DeepScan(List<string> data, string attack)
        {
            attack += " ";
            while (attack.Length > 0)
            {
                tokens.Add(attack.Substring(0, attack.IndexOf(' ')));
                attack = attack.Substring(attack.IndexOf(' ') + 1);
                attack = attack.Substring(attack.IndexOf(' ') + 1);
            }
            int yes = 0, no = 0;
            foreach (var s in tokens)
            {
                foreach (var mask in data)
                {
                    if (mask.Contains(s) && mask.Contains("not"))
                        no++;
                    if (mask.Contains(s) && !mask.Contains("not"))
                        yes++;
                }
            }
            if (yes == 0 && no == 0)
                Console.WriteLine("Can't say anymore, but most likely it is not an attack");
            else
            {
                int percent = 100 * yes / (yes + no);
                if (percent <= 25)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("It's not attack");
                    Console.ResetColor();
                }
                if (percent > 25 && percent <= 50)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Maybe it is an attack. Be careful");
                    Console.ResetColor();
                }
                if (percent > 50 && percent <= 75)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Your system is exposed to serious risk. Run your defender system");
                    Console.ResetColor();
                }
                if (percent > 75)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Attack detected!!! ");
                    Console.ResetColor();
                }
            }
        }
    }
}
