using SiteInterface;
using Forecast;
using Newtonsoft.Json.Linq;
using RequestManagement;

namespace Sites
{
    public class TomorrowIO : ISite
    {
        public string Name { get; private set; }
        public WeatherParameter Parameter { get; private set; }
        private string endPointUrl = "https://api.tomorrow.io/v4/timelines";
        private string location = "59.9386,30.3141";
        private string timeZone = "Europe/Moscow";
        private string fields = "temperature,windSpeed,windDirection,cloudCover";
        private string timeSteps;
        private string startTime;
        private string endTime;
        private string units = "metric";
        private string apiKey = "X16ljy5DRpBuuRfHIRUGsqA98qPGydmg";
        private string requestUrl;

        public TomorrowIO(WeatherParameter parameter)
        {
            Parameter = parameter;
            Name = "TomorrowIO";
            ChangeParameter(parameter);
        }

        public TomorrowIO() : this (WeatherParameter.Current)
        {

        }

        public SiteWeatherForecast GetCityWeatherForecast()
        {
            IRequest request = new APIRequest(requestUrl);
            var response = request.Get();

            if (response == null || !request.Connected || response == "Site is down")
            {
                return new SiteWeatherForecast(Name, request.Response); ;
            }

            return ParserWeatherData(request.Response);
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
            var time = DateTime.Now;
            Parameter = parameter;

            switch (parameter)
            {
                case WeatherParameter.Current:
                    timeSteps = "current";
                    requestUrl = endPointUrl + "?" + $"location={location}&timezone={timeZone}&fields={fields}&timesteps={timeSteps}&units={units}&apikey={apiKey}";
                    break;

                case WeatherParameter.Week:
                    startTime = time.ToString("s") + "Z";
                    timeSteps = "1d";
                    endTime = time.AddDays(6).ToString("s") + "Z";
                    requestUrl = endPointUrl + "?" + $"location={location}&timezone={timeZone}&startTime={startTime}&endTime=" +
                                 $"{endTime}&fields={fields}&timesteps={timeSteps}&units={units}&apikey={apiKey}";
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
            var currentWeatherData = jsonData["data"]["timelines"][0]["intervals"];

            foreach (var day in currentWeatherData)
            {
                var values = day["values"];
                var celsiusTemperature = values["temperature"].ToString();
                var humidity = "No data";
                var cloudCover = values["cloudCover"].ToString();
                var windSpeed = values["windSpeed"].ToString();
                var windDirection = values["windDirection"].ToString();
                var newDay = new CityWeatherForecast(celsiusTemperature, cloudCover, humidity, windDirection, windSpeed);

                forecasts.Add(newDay);
            }
            
            return new SiteWeatherForecast(Name, "No errors", forecasts);
        }
    }
}