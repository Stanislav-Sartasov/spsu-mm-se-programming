using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class Wc : ICommand
    {
        public bool RequiredArgs { get; private set; } = true;
        public bool NeedToBePrinted { get; private set; } = true;
        public bool IsSystem { get; private set; } = false;


        public string[] Run(string[] args)
        {
            try
            {
                FileManager.File file = new FileManager.File(args[0]);
                string[] output = new string[] { file.LinesCount.ToString(), file.WordsCount.ToString(), file.Weight.ToString() };

                return output;
            }
            catch
            {
                Console.WriteLine("Wc command: something wrong with path or file. Please try again");

                return new string[0];
            }
        }
    }
}
