using SiteInterface;
using Forecast;
using Newtonsoft.Json.Linq;
using RequestManagement;

namespace Sites
{
    public class OpenWeather : ISite    
    {
        public string Name { get; private set; }
        public WeatherParameter Parameter { get; private set; }
        private string endPointUrl = "https://api.openweathermap.org/data/2.5/onecall";
        private string latitude = "59.9386";
        private string longitude = "30.3141";
        private string exclude;
        private string units = "metric";
        private string lang = "en";
        private string apiKey = "8611f6b37c0de72547c2bae0011a4fd7";
        private string requestUrl;

        public OpenWeather(WeatherParameter parameter)
        {
            Parameter = parameter;
            Name = "OpenWeather";
            ChangeParameter(parameter);
            requestUrl = endPointUrl + "?" + $"lat={latitude}&lon={longitude}&units={units}&exclude={exclude}&lang={lang}&appid={apiKey}";
        }

        public OpenWeather() : this(WeatherParameter.Current)
        {
            
        }

        public SiteWeatherForecast GetCityWeatherForecast()
        {
            IRequest request = new APIRequest(requestUrl);
            var response = request.Get();

            if (response == null || !request.Connected || response == "Site is down")
            {
                return new SiteWeatherForecast(Name, response);
            }

            return ParserWeatherData(response);
        }

        public SiteWeatherForecast GetCityWeatherForecast(IRequest request)
        {
            var response = request.Get();

            if (response == null || !request.Connected || response == "Site is down")
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
                    exclude = "minutely,hourly,daily,alerts";
                    break;

                case WeatherParameter.Week:
                    exclude = "minutely,hourly,current,alerts";
                    break;
            }
        }

        private SiteWeatherForecast ParserWeatherData(string response)
        {
            var jsonData = JObject.Parse(response);

            if (jsonData == null)
            {
                return new SiteWeatherForecast(Name, "Site is down"); ;
            }

            var forecasts = new List<CityWeatherForecast>();
            JToken currentWeatherData;

            if (Parameter == WeatherParameter.Current)
            {
                currentWeatherData = jsonData["current"];
                var celsiusTemperature = currentWeatherData["temp"].ToString();
                var humidity = currentWeatherData["humidity"].ToString();
                var cloudCover = currentWeatherData["clouds"].ToString();
                var windSpeed = currentWeatherData["wind_speed"].ToString();
                var windDirection = currentWeatherData["wind_deg"].ToString();
                var newDay = new CityWeatherForecast(celsiusTemperature, cloudCover, humidity, windDirection, windSpeed);
                forecasts.Add(newDay);

                return new SiteWeatherForecast(Name, "No errors", forecasts);
            }
            else
            {
                currentWeatherData = jsonData["daily"];
            }

            int counter = 0;

            foreach (var day in currentWeatherData)
            {
                var celsiusTemperature = day["temp"]["day"].ToString();
                var humidity = day["humidity"].ToString();
                var cloudCover = day["clouds"].ToString();
                var windSpeed = day["wind_speed"].ToString();
                var windDirection = day["wind_deg"].ToString();

                var newDay = new CityWeatherForecast(celsiusTemperature, cloudCover, humidity, windDirection, windSpeed);
                forecasts.Add(newDay);

                counter++;

                if (counter == 7)
                {
                    break;
                }
            }


            return new SiteWeatherForecast(Name, "No errors", forecasts);
        }
    }
}