using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guard
{
    public class Commander
    {
        public List<Command> commands = new List<Command>();

        public void LoadCommands()
        {
            commands.Add(new Command("help","Output all possible commands"));
            commands.Add(new Command("cls", "Clear console"));
            commands.Add(new Command("exit", "Exit the program"));
            commands.Add(new Command("scan", "Scanning and Analyze. Do it with parameters -q,-d,-t,-r"));
            commands.Add(new Command("set","Set the time for scannig system"));
        }
    }
}
