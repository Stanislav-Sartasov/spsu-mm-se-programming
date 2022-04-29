using SiteInterface;
using Forecast;
using Newtonsoft.Json.Linq;
using RequestManagement;

namespace Sites
{
    public class StormGlass : ISite
    {
        public string Name { get; private set; }
        public WeatherParameter Parameter { get; private set; }
        private string endPointUrl = "https://api.stormglass.io/v2/weather/point";
        private string latitude = "59.9386";
        private string longitude = "30.3141";
        private string parameters = "airTemperature,cloudCover,humidity,windDirection,windSpeed";
        private string source = "noaa";
        private string start;
        private string end;
        private string apiKey = "37361b7a-bfea-11ec-a1b6-0242ac130002-37361bfc-bfea-11ec-a1b6-0242ac130002";
        private string requestUrl;

        public StormGlass(WeatherParameter parameter)
        {
            Parameter = parameter;
            int timeOffset = 3 * 60 * 60;
            Name = "StormGlass";
            start = (((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + timeOffset).ToString();
            ChangeParameter(parameter);
            requestUrl = endPointUrl + "?" + $"lat={latitude}&lng={longitude}&params={parameters}&source={source}&start={start}&end={end}&key={apiKey}";
        }

        public StormGlass() : this(WeatherParameter.Current)
        {

        }

        public SiteWeatherForecast GetCityWeatherForecast()
        {
            IRequest request = new APIRequest(requestUrl);
            var response = request.Get();

            if (response == null || !request.Connected || response == "Site does not response")
            {
                return new SiteWeatherForecast(Name, request.Response);
            }

            return ParserWeatherData(request.Response);
        }

        public SiteWeatherForecast GetCityWeatherForecast(IRequest request)
        {
            var response = request.Get();

            if (response == null || !request.Connected || response == "Site does not response")
            {
                return new SiteWeatherForecast(Name, response);
            }

            return ParserWeatherData(response);
        }

        public void ChangeParameter(WeatherParameter parameter)
        {
            Parameter = parameter;

            switch (parameter)
            {
                case WeatherParameter.Current:
                    end = start;
                    break;

                case WeatherParameter.Week:
                    int weekOffset = 6 * 24 * 60 * 60;
                    end = (Convert.ToInt64(start) + weekOffset).ToString();
                    break;
            }
        }

        private SiteWeatherForecast ParserWeatherData(string response)
        {
            var jsonData = JObject.Parse(response);

            if (jsonData == null)
            {
                return new SiteWeatherForecast(Name, "Response is not a Json file");
            }

            var forecasts = new List<CityWeatherForecast>();

            var currentWeatherData = jsonData["hours"];
            var counter = Parameter == WeatherParameter.Current ? 1 : 24;

            for (int i = 0; i < currentWeatherData.Count(); i += counter)
            {
                var day = currentWeatherData[i];
                var celsiusTemperature = day["airTemperature"]["noaa"].ToString();
                var humidity = day["humidity"]["noaa"].ToString();
                var cloudCover = day["cloudCover"]["noaa"].ToString();
                var windSpeed = day["windSpeed"]["noaa"].ToString();
                var windDirection = day["windDirection"]["noaa"].ToString();
                var newDay = new CityWeatherForecast(celsiusTemperature, cloudCover, humidity, windDirection, windSpeed);

                forecasts.Add(newDay);
            }
            
            return new SiteWeatherForecast(Name, "No errors", forecasts);
        }
    }
}
