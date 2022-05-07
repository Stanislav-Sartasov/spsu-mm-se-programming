using System;
using System.Diagnostics;
using System.IO;

namespace CommandLib
{
    public class UNKNOWNCommand : ACommand
    {
        public string Name;
        public UNKNOWNCommand(string unknowName)
        {
            Name = unknowName;
        }
        public override void Run()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = Name;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                try
                {
                    process.Start();
                    stdOut = process.StandardOutput.ReadToEnd();
                }
                catch (Exception)
                {
                    error.StdErr = 127;
                    error.Message = $"{Name}: command not found\n";
                }
            }
        }
    }
}