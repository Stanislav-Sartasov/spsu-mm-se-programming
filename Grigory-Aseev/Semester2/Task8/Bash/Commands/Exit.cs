namespace Bash.Commands
{
    internal class Exit : ICommand
    {
        public string Name { get; private set; }

        public Exit()
        {
            Name = "exit";
        }

        public string[]? Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Environment.Exit(0);
            }
            else if (args.Length == 1 && int.TryParse(args[0], out int result))
            {
                Environment.Exit(result);
            }
            else
            {
                Environment.Exit(-1);
            }

            return null;
        }
    }
}
