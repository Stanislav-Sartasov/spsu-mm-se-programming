using System.Text;

namespace Bash
{
    public class ConsoleOutputManager
    {
        private static ConsoleOutputManager instance;
        private ConsoleOutputManager()
        {

        }

        public static ConsoleOutputManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ConsoleOutputManager();
            }
                
            return instance;
        }
        public static string GetConsoleOutput(List<string> input, string lastBashCommand)
        {
            if (input.Count == 1 && input[0] == String.Empty)
            {
                return String.Empty;
            }

            var separator = GetArgumentsSeparator(lastBashCommand);
            var builder = new StringBuilder();

            foreach (var argument in input)
            {
                builder.Append(argument);
                builder.Append(separator);
            }

            return builder.AppendLine().ToString();
        }

        private static string GetArgumentsSeparator(string lastBashCommand)
        {
            switch (lastBashCommand)
            {
                case "ls":
                {
                    return "\t";
                }
                default:
                {
                    return "\n";
                }
            }
        }
    }
}
