namespace PCApp
{
	public static class ArgumentsParser
	{
		public static int Parse(string arg)
		{
			if (Int32.TryParse(arg, out var parsed) && (parsed > 0))
			{
				return parsed;
			}
			else
			{
				throw new ArgumentException("One of arguments is not a positive integer.");
			}
		}
	}
}
