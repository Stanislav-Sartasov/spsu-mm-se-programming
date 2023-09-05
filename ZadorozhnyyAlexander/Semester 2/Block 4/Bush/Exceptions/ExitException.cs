namespace Exceptions
{
    public class ExitException : Exception
    {
        public int ExitCode { get; private set; }

        public ExitException(int exitCode)
        {
            ExitCode = exitCode;
        }
    }
}
