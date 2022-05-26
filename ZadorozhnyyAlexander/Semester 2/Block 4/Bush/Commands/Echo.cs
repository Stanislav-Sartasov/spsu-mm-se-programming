using AbstractOperators;


namespace Commands
{
    public class Echo : ACommand
    {
        public override List<String> Execute(List<String> args, List<String> input)
        {
            return args.Count > 0 ? new List<String> { String.Join(" ", args) } : new List<String>();
        }
    }
}
