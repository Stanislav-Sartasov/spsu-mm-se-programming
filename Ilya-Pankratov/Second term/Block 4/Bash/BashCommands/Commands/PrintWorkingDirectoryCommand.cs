namespace BashCommands
{
    public class PrintWorkingDirectoryCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public PrintWorkingDirectoryCommand()
        {
            FullName = "PrintWorkingDirectory";
            ShortName = "pwd";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments != null)
            {
                return new List<string>() { "Invalid arguments" };
            }

            return new List<string>() { Directory.GetCurrentDirectory() };
        }
    }
}
