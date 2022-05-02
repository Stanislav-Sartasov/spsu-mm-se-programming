using System;
using System.IO;
using System.Text;

namespace BABASH
{
    public class WCCommand : Command
    {
        public WCCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "wc";
            parametres = args;
        }
        public override void Execute()
        {
            if (parametres.Length == 0)
            {
                var info = new StringInfo(stdIn);
                stdOut = "\n" + info.GetAllInfo();
            }
            else if (parametres.Length == 1)
            {
                string text = ReadFile(parametres[0]);
                var info = new StringInfo(text);
                stdOut = "\n" + info.GetAllInfo();
            }
            else
            {
                int totalWords = 0;
                int totalStrings = 0;
                int totalBytes = 0;
                foreach (string path in parametres)
                {
                    string text = ReadFile(path);
                    var info = new StringInfo(text);
                    totalWords += info.Words;
                    totalStrings += info.Strings;
                    totalBytes += info.Bytes;
                    stdOut += "\n" + info.GetAllInfo() + " " + path;
                    error.Message += "\n" + info.GetAllInfo() + " " + path;
                }
                stdOut += $"\n{totalStrings} {totalWords} {totalBytes} total";
                
            }
            error.Message = error.Message == null ? null : error.Message = error.Message[1..];
            stdOut = stdOut[1..];
        }
        public string ReadFile(string path)
        {
            string absolutePath = Path.GetFullPath(path, session.GetCurrentDirectory());
            try
            {
                return File.ReadAllText(absolutePath);
            }
            catch (UnauthorizedAccessException)
            {
                error.Message += $"\n{Name}: {path}: Is a directory";
                error.StdErr = 1;
            }
            catch (FileNotFoundException)
            {
                error.Message += $"\n{Name}: {path}: No such file or directory";
                error.StdErr = 1;
            }
            return null;
        }
    }
}