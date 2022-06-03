namespace Bash
{
    public class BashEmulator
    {
        private CommandHandler handler;

        public BashEmulator()
        {
            handler = new CommandHandler();
        }

        public IEnumerable<string> Execute(string? userInput)
        {
            var parsedInput = CommandParser.Parse(userInput);
            var commandsResult = handler.Execute(parsedInput);

            return commandsResult;
        }
    }
}