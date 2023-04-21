namespace Bash.Commands
{
    internal class PrintWorkingDirectory : ICommand
    {
        public string Name { get; private set; }

        public PrintWorkingDirectory()
        {
            Name = "pwd";
        }

        public string[]? Execute(string[] args) => new string[1] { Environment.CurrentDirectory };
    }
}