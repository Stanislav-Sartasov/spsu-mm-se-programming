using System.Text;

namespace Bash
{
    public static class CommandParser
    {
        public static List<string> Parse(string? commands)
        {
            if (commands == null || commands.Length == 0)
            {
                return new List<string> { String.Empty };
            }

            var userInput = commands.Trim();
            var builder = new StringBuilder();
            var bigArgumentFlag = false;
            var parsedCommands = new List<string>();

            foreach (var symbol in userInput)
            {
                if (bigArgumentFlag)
                {
                    if (symbol == '\"')
                    {
                        bigArgumentFlag = false;
                    }
                    else
                    {
                        builder.Append(symbol);
                    }
                }
                else if (symbol == '\"')
                {
                    bigArgumentFlag = true;
                }
                else if (symbol != ' ')
                {
                    builder.Append(symbol);
                }
                else 
                {
                    parsedCommands.Add(builder.ToString());
                    builder.Clear();
                }
            }

            if (builder.Length != 0)
            {
                parsedCommands.Add(builder.ToString());
            }

            parsedCommands = RemoveExcessSpaces(parsedCommands);

            if (parsedCommands == null || parsedCommands.Count == 0)
            {
                return new List<string>() { String.Empty };
            }
            else
            {
                return parsedCommands;
            }

        }

        private static List<string> RemoveExcessSpaces(List<string> commands)
        {
            return commands.Where(x => x != "").ToList();
        }
    }
}