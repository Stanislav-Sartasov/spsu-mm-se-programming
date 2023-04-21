using System;
using System.IO;

namespace CommandLib
{
    public class WCCommand : ACommand
    {
        public WCCommand(string[] args)
        {
            name = CommandName.wc;
            parametres = args;
        }
        public override void Run()
        {
            if (parametres.Length == 0)
            {
                var info = new StringInfo(stdIn);
                stdOut = info.GetAllInfo() + "\n";
            }
            else if (parametres.Length == 1)
            {
                string text = ReadFile(parametres[0]);
                var info = new StringInfo(text);
                stdOut = info.GetAllInfo() + "\n";
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
                    stdOut +=  info.GetAllInfo() + " " + path + "\n";
                }
                stdOut += $"{totalStrings} {totalWords} {totalBytes} total\n";
            }
            stdOut = stdOut == null ? null : stdOut[..^1];
        }
        public string ReadFile(string path)
        {
            string absolutePath = Path.GetFullPath(path, Environ.GetCurrentDirectory());
            try
            {
                return File.ReadAllText(absolutePath);
            }
            catch (UnauthorizedAccessException)
            {
                error.Message += $"{name}: {path}: Is a directory\n";
                error.StdErr = 1;
            }
            catch (FileNotFoundException)
            {
                error.Message += $"{name}: {path}: No such file or directory\n";
                error.StdErr = 1;
            }
            return null;
        }
    }
}