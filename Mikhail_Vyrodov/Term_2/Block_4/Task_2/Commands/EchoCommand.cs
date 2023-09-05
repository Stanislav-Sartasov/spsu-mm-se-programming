using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class EchoCommand : ICommand
    {
        public string Name { get; private set; } = "echo ";

        public string ApplyCommand(string[] arguments)
        {
            return arguments[0];
        }
    }
}
