using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Commands
{
    public class WcCommand : ICommand
    {
        public string Name { get; private set; } = "wc ";

        public string ApplyCommand(string[] arguments)
        {
            using (StreamReader reader = new StreamReader(arguments[0]))
            {
                string text = reader.ReadToEnd();
                uint wordCount = 1, strCount = 1;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ' || text[i] == '\n')
                        wordCount++;

                    if (text[i] == '\n')
                        strCount++;
                }
                string outputStr = String.Format("Words in file - {0}\n", wordCount);
                outputStr += String.Format("Strings in file - {0}\n", strCount);
                FileInfo fileInfo = new FileInfo(arguments[0]);
                outputStr += String.Format("File size in bytes - {0}", fileInfo.Length);
                return outputStr;
            }
        }
    }
}
