using Bash.Interfaces;

namespace Bash.BashComponents
{
    public class Logger : ILogger
    {
        public void Log(string? message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Console.WriteLine(message);
        }
    }
}