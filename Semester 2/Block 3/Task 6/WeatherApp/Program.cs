using System;
using System.Collections.Generic;
using ResponceReceiverLib;
using WeatherServicesLib;

namespace WeatherApp
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("This app gets information from different weather services and displays it.\n");

			Container container = new Container(new List<WeatherServices>{ WeatherServices.Openweather, WeatherServices.Stormglass });
			List<IWeatherService> services = container.GetAvailableServicesList();

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
						PrintWeatherForecast(WeatherServices.Stormglass, services);
						Console.WriteLine();
						break;
					}
					else if (firstResponce.Equals("2"))
					{
						Console.WriteLine();
						PrintWeatherForecast(WeatherServices.Openweather, services);
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
						PrintWeatherForecast(WeatherServices.Stormglass, services);
						Console.WriteLine();
					}
					else if (secondResponce.Equals("1") && firstResponce.Equals("2"))
					{
						Console.WriteLine();
						PrintWeatherForecast(WeatherServices.Openweather, services);
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

		static void PrintWeatherForecast(WeatherServices name, List<IWeatherService> services)
		{
			foreach (var service in services)
			{
				if (service.Name == name)
				{
					ResponceReceiver responce = new ResponceReceiver(service.URL);
					service.GetWeatherForecast(responce).Print();
					return;
				}
			}

			Console.WriteLine("Sorry, this service is switched off.");
		}
	}
}