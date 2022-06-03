namespace Bash.Commands
{
    internal class Clear : ICommand
    {
        public string Name { get; private set; }

        public Clear()
        {
            Name = "clear";
        }

        public string[]? Execute(string[] args)
        {
            Console.Clear();
            return null;
        }
    }
}
