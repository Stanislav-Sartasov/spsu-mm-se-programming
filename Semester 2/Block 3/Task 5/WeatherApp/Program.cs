using System;
using WebServicesLib;

namespace WeatherApp
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("This app gets information from different weather services and displays it.\n");

			IWebService stormglass = new Stormglass("59.9386", "30.3141", "660840ca-c942-11ec-9863-0242ac130002-66084142-c942-11ec-9863-0242ac130002");
			IWebService openweather = new Openweathermap("59.9386", "30.3141", "612ff6587b1aa1997d794833bb3c37ee");

			bool b = true;
			string firstResponce, secondResponce;

			do
			{
				while (true)
				{
					Console.WriteLine("Please, choose weather information service.\n- Enter 1 for stormglass.io\n- Enter 2 for openweathermap.org");
					firstResponce = Console.ReadLine();

					if (firstResponce.Equals("1"))
					{
						Console.WriteLine();
						stormglass.PrintResponce();
						Console.WriteLine();
						break;
					}
					else if (firstResponce.Equals("2"))
					{
						Console.WriteLine();
						openweather.PrintResponce();
						Console.WriteLine();
						break;
					}
					else
					{
						Console.WriteLine("\nIncorrect input. Please, enter 1 or 2\n");
						continue;
					}
				}

				while (true)
				{
					Console.WriteLine("What do you want to do next?\n- Enter 1 for information refreshing\n- Enter 2 for changing the resourse\n- Enter 3 for exit");
					secondResponce = Console.ReadLine();

					if (secondResponce.Equals("1") && firstResponce.Equals("1"))
					{
						Console.WriteLine();
						stormglass.PrintResponce();
						Console.WriteLine();
					}
					else if (secondResponce.Equals("1") && firstResponce.Equals("2"))
					{
						Console.WriteLine();
						openweather.PrintResponce();
						Console.WriteLine();
					}
					else if (secondResponce.Equals("2"))
					{
						Console.WriteLine();
						break;
					}
					else if (secondResponce.Equals("3"))
					{
						Console.WriteLine();
						b = false;
						break;
					}
					else
					{
						Console.WriteLine("\nIncorrect input. Please, enter 1, 2 or 3\n");
						continue;
					}
				}
			} while (b);

			Console.WriteLine("Thanks for using the app!");
		}
	}
}