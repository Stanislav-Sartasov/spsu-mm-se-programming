namespace Bash.Commands
{
    internal class Echo : ICommand
    {
        public string Name { get; private set; }

        public Echo()
        {
            Name = "echo";
        }

        public string[]? Execute(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                Console.Write($"{args[i]} ");
            }

            Console.Write($"{args[args.Length - 1]}\n");

            return null;
        }
    }
}
