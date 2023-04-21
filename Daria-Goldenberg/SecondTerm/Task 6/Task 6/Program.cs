namespace Task_6
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("This program prints the current weather in St.Petersburg." + Environment.NewLine +
					"The data is taken from the sites \"OpenWeather\" and \"Tomorrow.io\"." + Environment.NewLine +
					"If you want to get data from one site or both, enter name of the site." + Environment.NewLine +
					"If you want to exit the program, enter \"exit\"." + Environment.NewLine);

			List<ISite> sources;
			string[] names;				

			while (true)
			{
				while (true)
				{
					Console.Write("Enter the \"exit\" command or the name of the site(s): ");

					names = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

					if (names.Length > 2 || names.Length == 0)
						Console.WriteLine("Not enough or too much arguments.");
					else
						break;
				}

				if (names.Length == 2 && names[0] == names[1])
					sources = IoCContainer.GetRequest(new string[] { names[0] });
				else
					sources = IoCContainer.GetRequest(names);

				if (names[0].ToLower() == "exit")
					return;

				if (sources.Count == 0)
				{
					Console.WriteLine("Invalid input. Try again.");
					continue;
				}

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
		}
	}
}