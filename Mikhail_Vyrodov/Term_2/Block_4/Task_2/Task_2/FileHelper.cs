using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Task_2
{
    public static class FileHelper
    {
        public static string CatCommand(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
            else
            {
                return "No file found with this name";
            }
        }

        public static string WcCommand(string path)
        {
            using (StreamReader reader = new StreamReader(path))
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
                FileInfo fileInfo = new FileInfo(path);
                outputStr += String.Format("File size in bytes - {0}", fileInfo.Length);
                return outputStr;
            }
        }

        public static string OtherCommand(string path, string arguments="")
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists && fileInfo.Extension == ".exe")
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = path;
               
                if (arguments != "")
                {
                    procInfo.Arguments = path.Substring(path.IndexOf(' ') + 1);
                }
                try
                {
                    Process.Start(procInfo);
                }
                catch
                {
                    return "Something wrong went with the process";
                }
                return "Programm was started";
            }
            else
            {
                return "No program or command found with this name";
            }
        }
    }
}
