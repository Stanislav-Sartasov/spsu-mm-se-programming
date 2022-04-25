using static System.Console;

namespace Tools
{
    public class Painter
    {
        private readonly string siteName;
        public Painter(string siteName)
        {
            this.siteName = siteName;
        }

        public void DrawWeather(Weather.Weather weather)
        {
            if (weather == null)
            {
                WriteLine(siteName + "is down.");
                return;
            }
            WriteLine(siteName);
            WriteLine($"Temp: {weather.TempC} {weather.TempF}");
            WriteLine("Clouds: " + weather.Clouds);
            WriteLine("Humidity: " + weather.Humidity);
            WriteLine("Wind speed: " + weather.WindSpeed);
            WriteLine("Wind degree: " + weather.WindDegree);
            WriteLine("Fallouts: " + weather.FallOut);
        }
    }
}
