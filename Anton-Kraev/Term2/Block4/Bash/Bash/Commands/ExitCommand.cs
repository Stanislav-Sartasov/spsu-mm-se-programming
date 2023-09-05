namespace Bash.Commands
{
    public class ExitCommand : ICommand
    {
        public string Name => "exit";

        public string Execute(string args)
        {
            Environment.Exit(0);
            return "";
        }
    }
}