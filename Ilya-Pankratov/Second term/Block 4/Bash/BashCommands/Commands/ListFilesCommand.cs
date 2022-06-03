namespace BashCommands
{
    public class ListFilesCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public ListFilesCommand()
        {
            FullName = "ListFiles";
            ShortName = "ls";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments != null)
            {
                return new List<string>() { "Invalid arguments" };
            }

            var commandResult = new List<string>();
            var currentPath = Directory.GetCurrentDirectory();

            foreach (var entity in Directory.EnumerateDirectories(currentPath))
            {
                commandResult.Add(entity.Split("\\").Last());
            }

            foreach (var entity in Directory.EnumerateFiles(currentPath))
            {
                commandResult.Add(entity.Split("\\").Last());
            }

            if (commandResult.Count == 0)
            {
                return null;
            }

            return commandResult;
        }
    }
}
