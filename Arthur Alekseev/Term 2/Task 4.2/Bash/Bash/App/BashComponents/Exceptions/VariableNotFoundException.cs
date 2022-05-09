namespace Bash.App.BashComponents.Exceptions
{
	public class VariableNotFoundException : Exception
	{
		public VariableNotFoundException(string name) : base($"Variable {name} not found")
		{

		}
	}
}
