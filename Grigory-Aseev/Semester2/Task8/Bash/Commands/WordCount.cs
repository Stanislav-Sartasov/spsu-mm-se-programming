namespace Bash.Commands
{
    public class WordCount : ICommand
    {
        public string Name { get; private set; }

        public WordCount()
        {
            Name = "wc";
        }

        public string[]? Execute(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            List<string> result = new List<string>();

            foreach (var file in args)
            {
                try
                {
                    string currentPath = currentDir + "\\" + file;
                    int wordCount = 0;

                    var content = File.ReadLines(currentPath);

                    var linesCount = content.Count();
                    var bytesCount = new FileInfo(currentPath).Length.ToString();

                    foreach (var line in content)
                    {
                        wordCount += line.Split(" ").Count(word => word != "" && word != " " && word != "\t" && word != "\r" && word != "\a" && word != "\b" && word != "\n" && word != "\v" && word != "\f" && word != "\0");
                    }

                    result.AddRange(new string[] { $"Filename: {file}", $"Lines: {linesCount}\tWords: {wordCount}\tBytes: {bytesCount}" });
                }
                catch (Exception)
                {
                    result.Add($"Filename: {file} does not exist...");
                }
            }

            return result.ToArray();
        }
    }
}
