namespace Bash.Commands
{
    public class ListFiles : ICommand
    {
        public string Name { get; private set; }

        public ListFiles()
        {
            Name = "ls";
        }

        public string[]? Execute(string[] args)
        {
            List<string> result = new List<string>();

            try
            {
                result = GetNameFiles(Directory.GetFiles(Environment.CurrentDirectory));
                result.AddRange(GetNameFiles(Directory.GetDirectories(Environment.CurrentDirectory)));

                if (result.Count == 0)
                {
                    return new string[1] { "No files in this directory." };
                }

                return result.ToArray();
            }
            catch (Exception)
            {
                return new string[1] { "Failed to get files" };
            }

        }

        private List<string> GetNameFiles(string[] paths) => paths.Select(path => Path.GetFileName(path)).ToList();
    }
}
