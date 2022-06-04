using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_2
{
    public static class DirectoryHelper
    {
        public static string PrintDirectoryInfo()
        {
            string result = "";
            result += "Current directory:\n";
            result += String.Format("\t{0}\n", Directory.GetCurrentDirectory());
            result += "Inner directories:\n";
            foreach (string dir in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                result += String.Format("\t{0}\n", dir);
            }
            result += "Inner files:\n";

            foreach (string dir in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                result += String.Format("\t{0}\n", dir);
            }

            return result;
        }
    }
}
