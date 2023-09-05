namespace Task_8.Logger
{
	public class Logger : ILogger
	{
		public string Read()
		{
			return Console.ReadLine();
		}

		public void Write(string arg)
		{
			Console.Write(arg);
		}
	}
}
