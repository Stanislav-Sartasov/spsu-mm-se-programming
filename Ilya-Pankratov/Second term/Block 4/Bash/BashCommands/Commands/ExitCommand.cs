namespace BashCommands
{
    public class ExitCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public ExitCommand()
        {
            FullName = "Exit";
            ShortName = "exit";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments != null)
            {
                return new List<string>() { "Invalid arguments" };
            }

            Environment.Exit(0);
            return null;
        }
    }
}
