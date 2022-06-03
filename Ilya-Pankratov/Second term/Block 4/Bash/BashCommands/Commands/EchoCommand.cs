namespace BashCommands
{
    public class EchoCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public EchoCommand()
        {
            FullName = "Echo";
            ShortName = "echo";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments == null || !arguments.Any())
            {
                return new List<string>() { "Invalid arguments"};
            }

            var result = new List<string>();

            foreach (var argument in arguments)
            {
                result.Add(argument);
            }

            return result;
        }
    }
}
