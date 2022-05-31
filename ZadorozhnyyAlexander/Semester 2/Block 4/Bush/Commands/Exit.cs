using AbstractOperators;
using Exceptions;


namespace Commands
{
    public class Exit : ACommand
    {
        public override List<string> Execute(List<String> args, List<String> input)
        {
            if (args.Count == 0)
                throw new ExitException(0);

            throw int.TryParse(args[0], out int result) ? new ExitException(result) : new ExitException(0);
        }
    }
}
