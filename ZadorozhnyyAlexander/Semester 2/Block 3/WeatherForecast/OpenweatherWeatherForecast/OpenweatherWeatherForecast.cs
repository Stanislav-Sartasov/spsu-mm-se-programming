using AbstractWeatherForecast;
using RequestApi;
using DataParsers;


namespace OpenweatherWeatherForecast
{
    public class OpenweatherWeatherForecast : AWeatherForecast
    {
        private const string key = "b13eb5267bf4c9746e2f70d69a172b94";
        private const string url = "http://api.openweathermap.org/data/2.5/weather";

        private ApiHelper apiHelper;

        public OpenweatherWeatherForecast(HttpClient client) : base(client) { }

        protected override void Initialize()
        {
            apiHelper = new ApiHelper(CreateListParams(), key, url, (int)SiteTypes.Openweather, client);
            dataParser = new OpenweatherParser(apiHelper);
            isInitialized = !isInitialized;
        }

        protected override void ShowDescription()
        {
            Console.WriteLine("Weather forecast based on the Openweather website.\n" +
                "The data is presented in the form: 1-2) Temperature in Degrees, 3) CloudCover in percent 4) Humidity in percent\n" +
                "5) Precipitation in format mm/h 6) WindDirection in degrees from north(0) 7) WindSpeeed in format m/s");
        }

        private string[] CreateListParams()
        {
            string[] result = {
                $"?lat={saintPetesbergLat}",
                $"&lon={saintPetesbergLon}",
                $"&appid={key}",
                "&units=metric"
            };
            return result;
        }


    }
}