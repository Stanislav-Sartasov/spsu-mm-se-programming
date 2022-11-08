using Sites;
using System;
using WeatherContainer;

namespace WeatherForecast
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This program shows the weather in Peterhof");
			Console.WriteLine("Remember, sites have a limited number of requests");

			var sites = IoCContainer.GetServices();

			while (true)
			{
				Console.WriteLine("Command list: remove siteName, add siteNamen, display, exit\n" +
					"For example: add stormglass.io\nEnter the command:");
				switch (Console.ReadLine())
				{
					case "add tomorrow.io":
						IoCContainer.AddSites(typeof(TomorrowIo));
						Console.WriteLine("tomorrow.io added to the list");
						sites = IoCContainer.GetServices();
						continue;
					case "add stormglass.io":
						IoCContainer.AddSites(typeof(StormglassIo));
						Console.WriteLine("stormglass.io added to the list");
						sites = IoCContainer.GetServices();
						continue;
					case "remove tomorrow.io":
						IoCContainer.RemoveSites(typeof(TomorrowIo));
						Console.WriteLine("tomorrow.io removed from the list");
						sites = IoCContainer.GetServices();
						continue;
					case "remove stormglass.io":
						IoCContainer.RemoveSites(typeof(StormglassIo));
						Console.WriteLine("stormglass.io removed from the list");
						sites = IoCContainer.GetServices();
						continue;
					case "display":
						Console.Clear();
						Console.WriteLine($"The local time:{DateTime.Now}");
						foreach (var site in sites)
						{
							Console.WriteLine($"Weather according to {site}\n");
							site.ShowWeather();
						}
						continue;
					case "exit":
						return;
					default:
						Console.WriteLine("You have not changed the list of sites");
						continue;
				}
			}
		}
	}
}
