using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class Echo : ICommand
    {
        public bool RequiredArgs { get; private set; } = true;
        public bool NeedToBePrinted { get; private set; } = false;
        public bool IsSystem { get; private set; } = false;


        public string[] Run(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }

            return args;
        }
    }
}
