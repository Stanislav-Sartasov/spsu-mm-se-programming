namespace P2P.Loggers
{
    public interface ILogger
    {
        public void Log(string mes);

        public List<string> Logs { get; }
    }
}
