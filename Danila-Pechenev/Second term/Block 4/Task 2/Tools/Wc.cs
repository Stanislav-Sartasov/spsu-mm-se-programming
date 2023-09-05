namespace Tools;
using System.IO;

public class Wc : ICommand
{
    public string Name { get; } = "wc";

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
        if (stdinLine == "")
        {
            result = "0    0    0";
        }
        else
        {
            string[] lines = stdinLine.Split(Environment.NewLine);
            int countLines = lines.Length;
            int countWords = 0;
            int countBytes = System.Text.UTF8Encoding.UTF8.GetByteCount(stdinLine);

            foreach (var line in lines)
            {
                countWords += line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList().Count;
            }

            result = $"{countLines}    {countWords}    {countBytes}";
        }
 
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
                CountValues(path, out int countLines, out int countWords, out int countBytes);
                result += $"{countLines}    {countWords}    {countBytes}" + Environment.NewLine;
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

    private void CountValues(string path, out int countLines, out int countWords, out int countBytes)
    {
        var fileInfo = new FileInfo(path);
        countLines = 0;
        countWords = 0;
        countBytes = (int)fileInfo.Length;

        var sr = new StreamReader(path);
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line != null)
            {
                countLines++;
                countWords += line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList().Count;
            }
        }

        sr.Close();
    }
}
