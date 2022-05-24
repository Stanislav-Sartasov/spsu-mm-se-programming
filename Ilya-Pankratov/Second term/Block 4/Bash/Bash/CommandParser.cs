using System.Text;

namespace Bash
{
    public static class CommandParser
    {
        public static string[] Parse(string commands)
        {
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

            parsedCommands.Add(builder.ToString());
            return RemoveExcessSpaces(parsedCommands);
        }

        private static string[] RemoveExcessSpaces(List<string> commands)
        {
            return commands.Where(x => x != "").ToArray();
        }
    }
}