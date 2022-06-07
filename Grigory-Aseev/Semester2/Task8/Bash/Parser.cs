using System.Text;

namespace Bash
{
    public static class Parser
    {
        static StringBuilder builder = new StringBuilder();

        public static List<string[]>? GetCommands(string? userInput)
        {
            if (userInput?.Count(symb => symb == '"') % 2 != 0 || userInput is null)
            {
                return null;
            }

            List<string[]> result = new List<string[]>();

            foreach (var command in Parse(userInput, '|'))
            {
                result.Add(Parse(command, ' '));
            }

            result = result.Where(strs => strs is not null && strs.Length > 0).ToList();

            return DeleteQuotes(result);
        }

        private static string[] Parse(string str, char separator)
        {
            str = str.Trim() + separator;
            List<string> result = new List<string>();
            builder.Clear();
            bool insideWordFlag = false;

            foreach (var symbol in str)
            {
                if (symbol == separator && !insideWordFlag)
                {
                    result.Add(builder.ToString());
                    builder.Clear();
                }
                else
                {
                    builder.Append(symbol);
                    if (symbol == '"')
                    {
                        insideWordFlag = !insideWordFlag;
                    }
                }
            }

            return result.Where(str => str != "" && str != " ").ToArray();
        }

        private static List<string[]> DeleteQuotes(List<string[]> commands)
        {
            List<string[]> result = new List<string[]>();
            builder.Clear();
            List<string> resultBLock = new List<string>();

            foreach (var command in commands)
            {
                foreach (var block in command)
                {
                    foreach (var symbol in block)
                    {
                        if (symbol != '"')
                        {
                            builder.Append(symbol);
                        }
                    }

                    resultBLock.Add(builder.ToString());
                    builder.Clear();
                }

                result.Add(resultBLock.ToArray());
                resultBLock.Clear();
            }

            return result;
        }
    }
}