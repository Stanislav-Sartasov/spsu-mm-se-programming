namespace Task_8
{
	public static class CommandParser
	{
		public static List<string> ParseCommands(string arg)
		{
			return arg.Split("|").ToList();
		}
	}
}