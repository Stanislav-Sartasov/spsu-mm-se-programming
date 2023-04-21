using Enumerations;

namespace Task_1
{
    public class Parser
    {
        private string data;
        private readonly string[] possibleCommands = new string[] { "echo", "exit", "pwd", "cat", "wc", "whoami", "cd", "|", "clear" };
        private List<(string str, TypeOfParsedStr type)> parsedStr;
        public bool IsParsed { get; private set; }
        public string ErrorMessenge { get; private set; }

        public Parser(string data)
        {
            this.data = data;
            IsParsed = false;
            ErrorMessenge = "Parser was not called";
            parsedStr = new List<(string str, TypeOfParsedStr type)>();
        }

        public void Parse()
        {
            var isFirstCommand = true;
            var str = data.Split();
            var parsed = new List<(string str, TypeOfParsedStr type)>();
            IsParsed = true;

            for (var i = 0; i < str.Length; i++)
            {
                var argument = "";
                if (str[i] == "|")
                {
                    parsed.Add((str[i], TypeOfParsedStr.PipeLine));
                }

                else if (possibleCommands.Contains(str[i]))
                {
                    parsed.Add((str[i], TypeOfParsedStr.Command));
                }

                else if (str[i].First() == '\"')
                {
                    do
                    {
                        argument += " " + str[i];
                    } while (++i < str.Length && argument.Last() != '\"');

                    i--;

                    if (argument.Last() != '\"')
                    {
                        IsParsed = false;
                        ErrorMessenge = "Missing closing \"";
                        break;
                    }

                    argument = argument.Trim('\"');
                    parsed.Add((new string(argument.Skip(2).ToArray()), TypeOfParsedStr.Argument));
                }

                else if (str[i].First() == '$')
                {
                    var arg = str[i];
                    var command = arg.Split('=');
                    if (command.Length != 2)
                    {
                        IsParsed = false;
                        ErrorMessenge = "Too many/much '=' for declaring local variable";
                        break;
                    }

                    if (arg.Contains('\"'))
                    {
                        while (++i < str.Length && arg.Last() != '\"')
                        {
                            arg += " " + str[i];
                        }

                        i--;

                        if (arg.Last() != '\"')
                        {
                            IsParsed = false;
                            ErrorMessenge = "Missing closing \"";
                            break;
                        }

                        arg = new string(arg.Where(x => x != '\"').ToArray());
                    }

                    parsed.Add((new string(arg.Skip(1).ToArray()), TypeOfParsedStr.DeclaringLocalVariable));
                }
                
                else if (isFirstCommand)
                {
                    parsed.Add((str[i], TypeOfParsedStr.ProcessStart));
                }

                else
                {
                    parsed.Add((str[i], TypeOfParsedStr.Argument));
                }

                isFirstCommand = false;
            }

            if (IsParsed) parsedStr = parsed;
        }

        public List<(string str, TypeOfParsedStr type)> GetParsedString()
        {
            return new List<(string str, TypeOfParsedStr type)>(parsedStr);
        }
    }
}
