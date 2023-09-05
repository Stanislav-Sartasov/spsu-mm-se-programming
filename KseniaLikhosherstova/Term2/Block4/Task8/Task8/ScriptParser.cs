using System.Text.RegularExpressions;

namespace Task8
{
    public class ScriptParser
    {

        private static IDictionary<string, string> variables = null;
        public static IDictionary<string, string> CreateDict()
        {
            if (variables == null)
            {
                variables = new Dictionary<string, string>();
            }
            return variables;
        }

        public List<Command> Commands { get; private set; } = new List<Command>();

        public void Parse(string data)
        {
            var lines = data.Split(Environment.NewLine);

            ParseLines(lines);
        }

        public void ParseFromFile(string filePath)
        {
            var fileLines = File.ReadAllLines(filePath);

            ParseLines(fileLines);
        }

        private void ParseLines(string[] lines)
        {
          
            foreach (var line in lines)
            {
                var trimmedLine = line?.Trim();

                if (string.IsNullOrEmpty(trimmedLine)) continue;

                if (trimmedLine.StartsWith("$"))
                {
                    var indexOfEquals = trimmedLine.IndexOf('=');

                    if (indexOfEquals < 0)
                        throw new FormatException("Variable assignment symbol ('=') not found");

                    var value = trimmedLine.Substring(indexOfEquals + 1, trimmedLine.Length - indexOfEquals - 1);

                    var variableNames = trimmedLine.Substring(0, indexOfEquals)?.Split(',');
                    foreach (var variableName in variableNames)
                    {
                        var val = value.Trim();
                        var name = variableName.Trim();
                        if (!CreateDict().ContainsKey(name))
                            CreateDict().Add(name, val);
                        else
                            CreateDict()[name] = val;
                    }                
                    continue;  
                }
                var command = ParseCommand(line);

                if (command == null) continue;

                Commands.Add(command);
            }
        }

        private Command ParseCommand(string line)
        {

            var indexOfArguments = line.IndexOf(' ');

            var command = new Command();

            if (indexOfArguments < 0)
                command.Name = line;
            else
            {
                command.Name = line.Substring(0, indexOfArguments);

                var arguments = line.Substring(indexOfArguments + 1, line.Length - indexOfArguments - 1);

                ParseCommandArgument(command, arguments);
            }

            return command;
        }

        private void ParseCommandArgument(Command command, string arguments)
        {
            var pipeCommands = arguments.Split('|');

            if (pipeCommands.Length > 1)
            {
                var pipeCommandArguments = string.Join('|', pipeCommands.Skip(1))?.Trim();

                command.PipeCommand = ParseCommand(pipeCommandArguments);
            }

            var currentCommandArgs = pipeCommands[0].Trim();
          
            foreach (var variable in CreateDict().OrderByDescending(x=>x.Key.Length))
            {
                var pattern = Regex.Escape(variable.Key);
                if (Regex.IsMatch(currentCommandArgs, $@"(?<=^|\s){pattern}(?=\s|$)"))
                {
                    currentCommandArgs = currentCommandArgs.Replace(variable.Key, variable.Value);
                }
            }
            command.Arguments = currentCommandArgs;
        }
    }
}
