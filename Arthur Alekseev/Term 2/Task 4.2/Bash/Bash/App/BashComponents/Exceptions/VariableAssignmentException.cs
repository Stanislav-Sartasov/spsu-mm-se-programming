namespace Bash.App.BashComponents.Exceptions
{
	public class VariableAssignmentException : Exception
	{
		public VariableAssignmentException() : base("Assignment should be done like $var_name = value")
		{

		}
	}
}
