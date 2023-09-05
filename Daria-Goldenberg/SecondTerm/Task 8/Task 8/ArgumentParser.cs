namespace Task_8
{
    public static class ArgumentParser
    {
        public static List<string> ParseArguments(string arg)
        {
            var result = new List<string>() { "" };
            bool inBounds = false;

            foreach (var c in arg.Trim())
            {
                if (c == '"')
                {
                    inBounds = !inBounds;
                }
                else
                {
                    if (c == ' ' && !inBounds)
                    {
                        result[result.Count - 1] = result[result.Count - 1].Trim();
                        result.Add("");
                    }
                    else
                        result[result.Count - 1] += c;
                }
            }
            return result;
        }
    }
}