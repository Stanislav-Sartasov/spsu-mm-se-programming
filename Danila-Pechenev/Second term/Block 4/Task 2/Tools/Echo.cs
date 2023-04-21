namespace Tools;

public class Echo : ICommand
{
    public string Name { get; } = "echo";

    public bool RequiresArgumentsInStdinMode { get; } = true;

    bool ICommand.Execute(List<string>? arguments, out string result, bool stdin, string stdinLine)
    {
        result = "";
        if (arguments == null)
        {
            return false;
        }

        foreach (var arg in arguments)
        {
            result += arg + " ";
        }

        result = (result.Length > 0) ? result.Remove(result.Length - 1) : result;
        return true;
    }
}
