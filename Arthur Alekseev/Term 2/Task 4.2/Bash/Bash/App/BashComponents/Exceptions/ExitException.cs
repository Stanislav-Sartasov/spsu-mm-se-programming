namespace Bash.App.BashComponents.Exceptions
{
	public class ExitException : Exception
	{
		public readonly int ExitCode;

		public ExitException(int exitCode) : base()
		{
			ExitCode = exitCode;
		}
	}
}
