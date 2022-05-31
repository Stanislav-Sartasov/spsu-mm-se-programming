using AbstractOperators;

namespace CommandManager
{
    public class Executor
    {
        private Dictionary<String, ACommand> commands;
        private ARunner runner;

        public Executor(Dictionary<String, ACommand> commands, ARunner runner)
        {
            this.commands = commands;
            this.runner = runner;
        }

        private List<String> RunProcess(String cmd, List<String> args, List<String> input)
        {
            input.AddRange(args);
            return runner.Start(cmd, String.Join(" ", input));
        }

        public List<String> Execute(String cmd, List<String> args, List<String> input)
        {
            List<String> data;

            if (cmd == "")
                return new List<String>();

            try
            {
                data = commands[cmd].Execute(args, input);
            }
            catch (KeyNotFoundException)
            {
                data = RunProcess(cmd, args, input);
            }

            return data;
        }
    }
}
