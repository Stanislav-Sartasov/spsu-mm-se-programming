namespace Tools;
using System.IO;

public class Cd : ICommand
{
    public string Name { get; } = "cd";

    public bool RequiresArgumentsInStdinMode { get; } = true;

    bool ICommand.Execute(List<string>? arguments, out string result, bool stdin, string stdinLine)
    {
        result = "";
        if (arguments == null || arguments.Count == 0)
        {
            return false;
        }

        string path = arguments[0];
        if (Directory.Exists(path))
        {
            Directory.SetCurrentDirectory(path);
            return true;
        }
        else
        {
            result = "path does not exist";
            return false;
        }
    }
}
