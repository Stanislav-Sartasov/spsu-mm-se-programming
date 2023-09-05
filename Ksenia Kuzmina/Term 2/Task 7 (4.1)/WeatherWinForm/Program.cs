using Weather;
using Weather.Parsers;

namespace WeatherWinForm
{
	internal static class Program
	{
		[STAThread]
		static async Task Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new MainForm());
		}
	}
}