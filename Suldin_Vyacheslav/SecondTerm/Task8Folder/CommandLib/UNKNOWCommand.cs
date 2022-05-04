using System.Diagnostics;
using System.IO;

namespace CommandLib
{
    public class UNKNOWCommand : ACommand
    {
        public string Name;
        public UNKNOWCommand(string unknowName)
        {
            Name = unknowName;
        }
        public override void Run()
        {
            string logFilePath = Environ.GetCurrentDirectory() + "\\LogFile.txt";
            File.Create(logFilePath).Close();
            File.WriteAllText(logFilePath, $"{Name}: command not found");
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = logFilePath;
            process.Start();
        }
    }
}