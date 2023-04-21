namespace Tools;
using System.IO;

public class Cat : ICommand
{
    public string Name { get; } = "cat";

    public bool RequiresArgumentsInStdinMode { get; } = false;

    bool ICommand.Execute(List<string>? arguments, out string result, bool stdin, string stdinLine)
    {
        if (arguments != null && arguments.Count > 0)
        {
            stdin = false;
        }

        if (stdin)
        {
            return ExecuteWithStdin(out result, stdinLine);
        }
        else
        {
            return ExecuteWithArguments(arguments, out result);
        }
    }

    private bool ExecuteWithStdin(out string result, string stdinLine)
    {
        result = stdinLine;
        return true;
    }

    private bool ExecuteWithArguments(List<string>? arguments, out string result)
    {
        result = "";
        if (arguments == null || arguments.Count == 0)
        {
            return false;
        }

        bool success = false;
        for (int i = 0; i < arguments.Count; i++)
        {
            string path = arguments[i];
            if (File.Exists(path))
            {
                result += WriteFileContent(path) + Environment.NewLine;
                success = true;
            }
            else
            {
                result += "path does not exist" + Environment.NewLine;
            }
        }

        result = (result.Length > 0) ? result.Remove(result.Length - Environment.NewLine.Length) : result;
        return success;
    }

    private string WriteFileContent(string path)
    {
        string result = "";
        var sr = new StreamReader(path);
        while (!sr.EndOfStream)
        {
            result += sr.ReadLine() + Environment.NewLine;
        }

        result = (result.Length > 0) ? result.Remove(result.Length - Environment.NewLine.Length) : result;
        sr.Close();
        return result;
    }
}
