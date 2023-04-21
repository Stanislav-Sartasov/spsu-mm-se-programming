using AbstractWeatherForecast;


namespace OpenweatherWeatherForecast
{
    public class OpenweatherForecast : AWeatherForecast
    {
        public OpenweatherForecast(AParser parser) : base(parser) { }

        protected override void ShowDescription()
        {
            Console.WriteLine("Weather forecast based on the Openweather website.\n" +
                "The data is presented in the form: 1-2) Temperature in Degrees, 3) CloudCover in percent 4) Humidity in percent\n" +
                "5) Precipitation in format mm/h 6) WindDirection in degrees from north(0) 7) WindSpeeed in format m/s");
        }
    }
}