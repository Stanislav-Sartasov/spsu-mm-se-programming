using System.Diagnostics;
using System.IO;

namespace BABASH
{
    public class UNKNOWCommand : Command
    {
        public UNKNOWCommand(string unknowName, Session session)
        {
            Name = unknowName;
            this.session = session;
        }
        public override void Execute()
        {
            string logFilePath = session.GetCurrentDirectory() + "\\LogFile.txt";
            File.Create(logFilePath).Close();
            File.WriteAllText(logFilePath, $"{Name}: command not found");
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = logFilePath;
            process.Start();
        }
    }
}