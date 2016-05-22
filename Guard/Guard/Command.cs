using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guard
{
    public class Command
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Command(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
