using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class RequestURLGetter
    {
        private const string latitude = "59.57";
        private const string longitude = "30.19";

        public Uri GetRequestURL(string site)
        {
            if (site == "tomorrow.io")
            {
                Uri initialURL = new Uri("https://api.tomorrow.io/v4/timelines?");
                string fields = "temperature,cloudCover,humidity,precipitationIntensity,windDirection,windSpeed";
                string apiKey = "AVMNJMtSlSrsgXtt1gIB6x2MrgKqIqxO";
                string allParameters = String.Format("?location={0},{1}&fields={2}&timesteps=current&units=metric&apikey={3}",
                    latitude, longitude, fields, apiKey);
                Uri finalURL = new Uri(initialURL, allParameters);
                return finalURL;
            }
            else
            {
                var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                string parameters = "airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed";
                string source = "noaa";
                Uri initialURL = new Uri("https://api.stormglass.io/v2/weather/point");
                string allParameters = String.Format("?lat={0}&lng={1}&params={2}&start={3}&end={4}&source={5}",
                    latitude, longitude, parameters, timeStamp, timeStamp, source);
                Uri finalURL = new Uri(initialURL, allParameters);
                return finalURL;
            }
        }
    }
}
