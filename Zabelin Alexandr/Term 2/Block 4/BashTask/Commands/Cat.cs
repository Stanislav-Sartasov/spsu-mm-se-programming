using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class Cat : ICommand
    {
        public bool RequiredArgs { get; private set; } = true;
        public bool NeedToBePrinted { get; private set; } = true;
        public bool IsSystem { get; private set; } = false;


        public string[] Run(string[] args)
        {
            try
            {
                FileManager.File file = new FileManager.File(args[0]);

                return new string[] { file.FileText };
            }
            catch
            {
                Console.WriteLine("Cat command: something wrong with path or file. Please try again");

                return new string[0];
            }
        }
    }
}
