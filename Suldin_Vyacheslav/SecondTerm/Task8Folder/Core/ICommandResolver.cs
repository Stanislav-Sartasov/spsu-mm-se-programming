namespace Core
{
    public interface ICommandResolver
    {
        public IResponse Resolve(string commandLine);
    }
}