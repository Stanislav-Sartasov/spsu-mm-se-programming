using System.Text;

namespace BashCommands
{
    public class WordCount : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public WordCount()
        {
            FullName = "WordCount";
            ShortName = "wc";
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

                using (var reader = new StreamReader(filePath))
                {
                    int wordCouner = 0;
                    int lineCounter = 0;
                    var byteCounter = new FileInfo(filePath).Length.ToString();

                    while (!reader.EndOfStream)
                    {
                        lineCounter++;
                        wordCouner += reader.ReadLine().Split(" ").Count(x => x != " " && x != "" && x != "\n" && x != "\r" && x != "\t");
                    }

                    oneArgumentResult.Append($"lines {lineCounter}\nwords {wordCouner}\nbytes {byteCounter}");
                }

                result.Add(oneArgumentResult.ToString());
                oneArgumentResult.Clear();
            }

            return result;
        }
    }
}
