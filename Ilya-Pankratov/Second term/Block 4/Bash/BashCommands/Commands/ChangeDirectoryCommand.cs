namespace BashCommands
{
    public class ChangeDirectoryCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public ChangeDirectoryCommand()
        {
            FullName = "ChangeDirectory";
            ShortName = "cd";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments == null || !arguments.Any() || arguments.Count() > 1)
            {
                return new List<string>() { "Invalid arguments" };
            }

            var argument = arguments.First();
            var diskName = DriveInfo.GetDrives().Where(x => x.Name.Contains(argument)).FirstOrDefault();
            string? newDirectory = null;

            if (argument == "..")
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());

                if (parentDirectory != null)
                {
                    newDirectory = parentDirectory.FullName;
                }
            }
            else if (diskName != null && Directory.Exists(diskName.Name))
            {
                newDirectory = diskName.Name;
            }
            else if (Directory.Exists(argument))
            {
                newDirectory = argument;
            }
            else
            {
                return new List<string>() { $"{argument} does not exist" };
            }

            if (newDirectory != null)
            {
                try
                {
                    Directory.SetCurrentDirectory(newDirectory);
                }
                catch (Exception e)
                {
                    return new List<string>() { e.Message };
                }
            }

            return null;
        }
    }
}