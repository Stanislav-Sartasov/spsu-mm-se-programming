namespace BashCommands
{
    public class DefaultCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public DefaultCommand()
        {
            FullName = "DefaultCommand";
            ShortName = "defcmd";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            string processResult;

            if (arguments == null || !arguments.Any())
            {
                return null;
            }

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = arguments.First();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                foreach (var arg in arguments.ToArray()[1..])
                {
                    process.StartInfo.ArgumentList.Add(arg);
                }

                try
                {
                    process.Start();
                    processResult = process.StandardOutput.ReadToEnd();
                }
                catch (Exception)
                {
                    processResult = $"Command {arguments.First()} was not found";
                }
            }

            return new List<string>() { processResult };
        }
    }
}
