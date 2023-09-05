using System.Text;
using System.Text.RegularExpressions;


namespace Task8.Commands
{
    internal class WcCommand : ICommand
    {
        public string Name { get { return "wc"; } }

        public StringBuilder Motion(string arguments)
        {
            StringBuilder result = new StringBuilder();
            var fileTextWc = ReadFileText(arguments);

            var bytesCount = File.ReadAllBytes(Path.GetFullPath(arguments)).Length;

            var linesCount = fileTextWc.Split(Environment.NewLine).Length;

            var wordsCount = Regex.Matches(fileTextWc, @"\w+").Count;

            result.AppendLine($"Lines count: {linesCount}");
            result.AppendLine($"Words count: {bytesCount}");
            return result.AppendLine($"Bytes count: {wordsCount}");

        }

        private string ReadFileText(string path)
        {
            var filePath = string.IsNullOrEmpty(path) ? string.Empty : Path.GetFullPath(path);

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("Argument PATH is null or empty");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not found");
            }

            return File.ReadAllText(filePath);
        }
    }
}

