using System.Text;


namespace Task8.Commands
{
    internal class CatCommand : ICommand
    {
        public string Name { get { return "cat"; } }

        public StringBuilder Motion(string arguments)
        {
            StringBuilder result = new StringBuilder();
            string fileText = ReadFileText(arguments);

            return result.AppendLine(fileText);

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
