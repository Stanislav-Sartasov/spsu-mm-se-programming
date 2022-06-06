using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Commands
{
    public class AnotherCommand : ICommand
    {
        public string Name { get; private set; } = "another";

        public string ApplyCommand(string[] arguments)
        {
            FileInfo fileInfo = new FileInfo(arguments[0]);
            if (fileInfo.Exists && fileInfo.Extension == ".exe")
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = arguments[0];

                if (arguments[1] != "")
                {
                    procInfo.Arguments = arguments[0].Substring(arguments[0].IndexOf(' ') + 1);
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
