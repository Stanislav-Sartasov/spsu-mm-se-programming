namespace Tools;
using System.IO;

public class Ls : ICommand
{
    public string Name { get; } = "ls";

    public bool RequiresArgumentsInStdinMode { get; } = false;

    bool ICommand.Execute(List<string>? arguments, out string result, bool stdin, string stdinLine)
    {
        if (arguments == null || arguments.Count == 0)
        {
            return GetResultByPath(Directory.GetCurrentDirectory(), out result);
        }
        else
        {
            string path = arguments[0];
            return GetResultByPath(path, out result);
        }
    }
        
    private bool GetResultByPath(string path, out string result)
    {
        result = "";
        if (Directory.Exists(path))
        {
            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                result += file.Name + Environment.NewLine;
            }

            var directories = directoryInfo.GetDirectories();
            if (result.Length > 0 && directories.Length > 0)
            {
                result += Environment.NewLine;
            }

            foreach (var directory in directories)
            {
                result += directory.Name + Environment.NewLine;
            }

            result = (result.Length > 0) ? result.Remove(result.Length - Environment.NewLine.Length) : result;
            return true;
        }

        result += "path does not exist";
        return false;
    }
}
