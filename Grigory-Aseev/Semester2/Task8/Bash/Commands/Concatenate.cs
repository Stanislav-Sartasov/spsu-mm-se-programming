namespace Bash.Commands
{
    public class Concatenate : ICommand
    {
        public string Name { get; private set; }

        public Concatenate()
        {
            Name = "cat";
        }

        public string[]? Execute(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            List<string> result = new List<string>();

            foreach (var file in args)
            {
                try
                {
                    var content = File.ReadLines(currentDir + "\\" + file);

                    result.Add($"Filename: {file}");

                    foreach (var line in content)
                    {
                        result.Add(line);
                    }
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
