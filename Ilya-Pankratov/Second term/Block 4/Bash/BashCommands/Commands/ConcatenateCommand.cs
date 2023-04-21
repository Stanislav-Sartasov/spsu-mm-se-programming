using System.Text;

namespace BashCommands
{
    public class ConcatenateCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public ConcatenateCommand()
        {
            FullName = "Concatenate";
            ShortName = "cat";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments == null || !arguments.Any())
            {
                return new List<string>() { "Invalid arguments" };
            }

            var oneArgumentResult = new StringBuilder();
            var result = new List<string>();

            foreach (var argument in arguments)
            {
                string filePath;

                if (File.Exists(argument))
                {
                    filePath = argument;
                }
                else
                {
                    oneArgumentResult.Append($"The file {argument} does not exist");
                    result.Add(oneArgumentResult.ToString());
                    oneArgumentResult.Clear();
                    continue;
                }

                var lines = File.ReadAllLines(filePath);

                for (int j = 0; j < lines.Length; j++)
                {
                    oneArgumentResult.Append(lines[j]);

                    if (j != lines.Length - 1)
                    {
                        oneArgumentResult.Append('\n');
                    }
                }

                result.Add(oneArgumentResult.ToString());
                oneArgumentResult.Clear();
            }

            return result;
        }
    }
}
