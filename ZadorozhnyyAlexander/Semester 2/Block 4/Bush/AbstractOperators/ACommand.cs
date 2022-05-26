namespace AbstractOperators
{
    public abstract class ACommand
    {
        public abstract List<String> Execute(List<String> args, List<String> input);
    }
}