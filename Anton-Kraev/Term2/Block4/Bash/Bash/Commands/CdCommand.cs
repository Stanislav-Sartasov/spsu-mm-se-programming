namespace Bash.Commands
{
    public class CdCommand : ICommand
    {
        public string Name => "cd";

        public string Execute(string args)
        {
            Directory.SetCurrentDirectory(args);
            return "";
        }
    }
}