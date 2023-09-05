
namespace Task_5
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("This program prints the current weather in St.Petersburg." + Environment.NewLine +
					"The data is taken from the sites \"OpenWeather\" and \"Tomorrow.io\"." + Environment.NewLine +
					"If you want to get or update the data, enter \"update\"." + Environment.NewLine +
					"If you want to exit the program, enter \"exit\"." + Environment.NewLine);

			Site[] sources = new Site[] { new OpenWeather(new Request()), new TomorrowIo(new Request()) };

			while (true)
			{
				Console.Write("Enter the command: ");

				string consoleInput = Console.ReadLine();

				if (consoleInput.ToLower() == "update")
				{
					foreach (var site in sources)
					{
						try
						{
							Logger.LogWeather(site.Name, site.GetData());
						}
						catch (Exception ex)
						{
							Logger.LogError(ex);
						}
					}
				}
				else if (consoleInput.ToLower() == "exit")
					return;
				else
				Console.WriteLine("Input error, try again.");
			}
		}
	}
}