using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;
using Parsers;
using System.Text.RegularExpressions;

namespace Task5
{
	public class ConsoleWriter
	{
		public static void ShowWeatherInfo(WeatherInformation info)
		{

			if (info.Error != null)
            {
				Console.WriteLine(info.Name + ": " + (ErrorType)Convert.ToInt32(Regex.Replace(info.Error, @"[^\d]+", "")));
            }
			else
            {
				foreach (var item in info.GetType().GetProperties().Where(x=>x.Name != "Error"))
				{
					string value = item.GetValue(info).ToString();
					Console.Write(value);
					Fill(value.Length);
				}
			}
			Console.WriteLine();
        }
		public static void Fill(int n)
		{
			for (int i = 0; i < (17 - n); i++)
				Console.Write(" ");
		}
	}
}
