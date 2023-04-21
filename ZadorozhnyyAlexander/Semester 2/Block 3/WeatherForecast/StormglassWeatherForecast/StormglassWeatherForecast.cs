using AbstractWeatherForecast;
using RequestApi;
using DataParsers;


namespace StormglassWeatherForecast
{
    public class StormglassWeatherForecast : AWeatherForecast
    {
        private const string key = "b720d1e4-c40a-11ec-844a-0242ac130002-b720d252-c40a-11ec-844a-0242ac130002";
        private const string url = "https://api.stormglass.io/v2/weather/point";

        private ApiHelper apiHelper;

        public StormglassWeatherForecast(HttpClient client) : base(client){ }

        protected override void Initialize()
        {
            apiHelper = new ApiHelper(CreateListParams(), key, url, (int)SiteTypes.Stormglass, client);
            dataParser = new StormglassParser(apiHelper);
            isInitialized = !isInitialized;
        }

        protected override void ShowDescription()
        {
            Console.WriteLine("Weather forecast based on the Stormglass website.\n" +
                "The data is presented in the form: 1-2) Temperature in Degrees, 3) CloudCover in percent 4) Humidity in percent\n" +
                "5) Precipitation in format mm/h 6) WindDirection in degrees from north(0) 7) WindSpeeed in format m/s");
        }

        private string[] CreateListParams()
        {
            string[] result = {
                $"?lat={saintPetesbergLat}",
                $"&lng={saintPetesbergLon}",
                "&params=",
                "airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed",
                $"&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}",
                $"&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}",
                "&source=sg",
            };
            return result;
        }
    }
}