using System.Diagnostics;

namespace Task_8.Commands
{
	public class StartApp : Command
	{
		public override CommandResult Run(List<string> args)
		{
			List<string> err = new List<string>();

			try
			{
				if (args[0] != "")
					if (args.Count > 1)
						Process.Start(args[0], args.ToArray()[1..]);
					else
						Process.Start(args[0]);
			}
			catch
			{
				if (args.Count >= 1 && !args[0].StartsWith("$"))
					err.Add("Process " + args[0] + " could not be started.");
			}

			return new CommandResult(err, new List<string> { });
		}
	}
}
