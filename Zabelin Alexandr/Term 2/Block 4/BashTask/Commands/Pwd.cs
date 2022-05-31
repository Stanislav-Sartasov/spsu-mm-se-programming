using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Commands
{
    public class Pwd : ICommand
    {
        public bool RequiredArgs { get; private set; } = false;
        public bool NeedToBePrinted { get; private set; } = true;
        public bool IsSystem { get; private set; } = false;


        public string[] Run(string[] args)
        {
            try
            {
                DirectoryManager.DirectoryInfo directoryInfo = new DirectoryManager.DirectoryInfo();
                string[] fullInfo = new string[directoryInfo.FileNamesInDirectory.Length + 1];

                fullInfo[0] = directoryInfo.CurrentDirectory;

                for (int i = 1; i < fullInfo.Length; i++)
                {
                    fullInfo[i] = directoryInfo.FileNamesInDirectory[i - 1];
                }

                return fullInfo;
            }
            catch
            {
                Console.WriteLine("Pwd command: something wrong with directory. Please try again");

                return new string[0];
            }
        }
    }
}
