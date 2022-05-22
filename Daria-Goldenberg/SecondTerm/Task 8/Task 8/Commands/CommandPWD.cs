namespace Task_8.Commands
{
	public class CommandPWD : Command
	{
		public override CommandResult Run(List<string> args)
		{
			List<string> output = new List<string>();
			List<string> errors = new List<string>();
			output.Add(Directory.GetCurrentDirectory());
			foreach (var file in Directory.EnumerateFiles(output[0]))
				output.Add(file.Split("\\")[^1]);
			
			return new CommandResult(errors, output);
		}
	}
}
