namespace Bash.Interfaces
{
    public interface IExecuter
    {
        public string? Execute(string command, string args);
    }
}