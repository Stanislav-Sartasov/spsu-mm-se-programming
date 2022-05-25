using System.Text;

namespace Bash
{
    public static class ConsoleOutputManager
    {
        public static string GetConsoleOutput(List<string> input)
        {
            if (input.Count == 1 && input[0] == String.Empty)
            {
                return String.Empty;
            }

            var builder = new StringBuilder();

            foreach (var argument in input)
            {
                builder.AppendLine(argument);
            }

            return builder.AppendLine().ToString();
        }
    }
}
