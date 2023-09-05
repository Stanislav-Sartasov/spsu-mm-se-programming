using System.Diagnostics;

namespace Bash.Commands
{
    public class NewProcess : ICommand
    {
        public string Name { get; private set; }

        public NewProcess()
        {
            Name = "np";
        }

        public string[]? Execute(string[] args)
        {
            try
            {
                return StartProcess(true, false, args);
            }
            catch (Exception)
            {
                try
                {
                    return StartProcess(false, true, args);
                }
                catch (Exception)
                {
                    return new string[1] { "Application not found" };
                }
            }

        }

        private string[]? StartProcess(bool output, bool shellFlag, string[] args)
        {
            var startInfo = new ProcessStartInfo() { FileName = args[0], RedirectStandardOutput = output, UseShellExecute = shellFlag, WindowStyle = ProcessWindowStyle.Maximized };

            for (int i = 1; i < args.Length; i++)
            {
                startInfo.ArgumentList.Add(args[i]);
            }

            using (var process = Process.Start(startInfo))
            {
                if (output)
                {
                    using (var reader = process?.StandardOutput)
                    {
                        string[] result = new string[1] { reader?.ReadToEnd() ?? "" };
                        process?.WaitForExit();
                        return result;
                    }
                }
                return null;
            }
        }
    }
}