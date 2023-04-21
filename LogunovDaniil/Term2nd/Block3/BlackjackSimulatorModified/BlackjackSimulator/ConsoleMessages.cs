namespace BlackjackSimulator
{
	public static class ConsoleMessages
	{
		public static void SendGreetings()
		{
			Console.WriteLine("This program tests Blackjack strategies that were given in a .dll file");
			Console.WriteLine("and ouptuts main data resulting from those tests.\n\n");
		}

		public static int SendError(int errorCode, string messsage)
		{
			Console.WriteLine("Error: " + messsage);
			return errorCode;
		}
	}
}
