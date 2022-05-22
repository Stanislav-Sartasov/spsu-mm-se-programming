namespace Task_8.Commands
{
	public class Cat : Command
	{
		public override CommandResult Run(List<string> args)
		{
			List<string> output = new List<string>();
			List<string> errors = new List<string>();

			foreach (string arg in args)
			{
				try
				{
					output.AddRange(File.ReadLines(arg));
				}
				catch 
				{
					errors.Add("Did not find the file " + arg + ".");
				}
			}
			return new CommandResult(errors, output);
		}
	}
}