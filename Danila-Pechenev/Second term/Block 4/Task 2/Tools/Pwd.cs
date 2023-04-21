namespace Tools;
using System.IO;

public class Pwd : ICommand
{
    public string Name { get; } = "pwd";

    public bool RequiresArgumentsInStdinMode { get; } = false;

    bool ICommand.Execute(List<string>? arguments, out string result, bool stdin, string stdinLine)
    {
        result = Directory.GetCurrentDirectory().Replace("\\", "/");
        return true;
    }
}
