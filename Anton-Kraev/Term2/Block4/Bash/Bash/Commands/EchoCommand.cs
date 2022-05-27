namespace Bash.Commands
{
    public class EchoCommand : ICommand
    {
        public string Name => "echo";

        public string Execute(string args)
        {
            return args;
        }
    }
}