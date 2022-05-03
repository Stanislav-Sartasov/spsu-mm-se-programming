namespace Core
{
    public interface ICommandResolver
    {
        public string Resolve(string commandLine);
    }
}