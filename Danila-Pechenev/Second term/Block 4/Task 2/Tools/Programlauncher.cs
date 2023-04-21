namespace Tools;
using System.Diagnostics;

public static class Programlauncher
{
    public static bool TryLaunchProgram(string programName, string? argumentsLine, out string text)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        var startInfo = new ProcessStartInfo();
        startInfo.RedirectStandardOutput = true;
        if (argumentsLine != null)
        {
            startInfo.Arguments = argumentsLine;
        }

        bool success;
        startInfo.WorkingDirectory = currentDirectory;
        var files = Directory.GetFiles(currentDirectory);
        success = TryRunInDirectory(programName, startInfo, files, out text);
        if (success)
        {
            return true;
        }

        string[]? paths = Environment.GetEnvironmentVariable("PATH")?.Split(";");
        Directory.SetCurrentDirectory("/");
        if (paths != null)
        {
            foreach (var path in paths)
            {
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch
                {
                    continue;
                }

                success = TryRunInDirectory(programName, startInfo, files, out text);
                if (success)
                {
                    Directory.SetCurrentDirectory(currentDirectory);
                    return true;
                }
            }
        }

        Directory.SetCurrentDirectory(currentDirectory);
        return text.Length > 0;
    }

    private static bool TryRunInDirectory(string programName, ProcessStartInfo startInfo, string[] files, out string text)
    {
        string? currentLine;
        text = "";
        foreach (var file in files)
        {
            if (file.Split('\\')[^1].Split('.')[0] == programName || file.Split('\\')[^1] == programName)
            {
                startInfo.FileName = file;
                Process? process = Process.Start(startInfo);
                if (process != null)
                {
                    process.WaitForExit();
                    while (!process.StandardOutput.EndOfStream)
                    {
                        currentLine = process.StandardOutput.ReadLine();
                        if (currentLine != null)
                        {
                            if (text.Length > 0)
                            {
                                text += Environment.NewLine;
                            }

                            text += currentLine;
                        }
                    }

                    return true;
                }
            }
        }

        return false;
    }
}
