namespace P2P.Loggers
{
    public class Logger : ILogger
    {
        public List<string> Logs { get; }

        public Logger()
        {
            Logs = new List<string>();
        }

        public void Log(string mes)
        {
            Logs.Add(mes);
        }
    }
}
