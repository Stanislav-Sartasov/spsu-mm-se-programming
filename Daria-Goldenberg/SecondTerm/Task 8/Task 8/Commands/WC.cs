namespace Task_8.Commands
{
	public class WC : Command
	{
		public override CommandResult Run(List<string> args)
		{
			List<string> output = new List<string>();
			List<string> errors = new List<string>();

			foreach (string arg in args)
			{
				try
				{
					string? data = File.ReadAllText(arg);
					long byteLength = new FileInfo(arg).Length;
					output.Add(data.Split(Environment.NewLine).Length.ToString() + " " 
						+ data.Split(" ").Length.ToString() + " " + byteLength.ToString());
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
