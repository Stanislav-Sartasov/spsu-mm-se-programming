using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class StormglassioRequestURLFiller : IRequestURLFiller
    {
        private const string latitude = "59.57";
        private const string longitude = "30.19";
        public void FillRequestURL(IWebServerHelper webHelper)
        {
            var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            string parameters = "airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed";
            string source = "noaa";
            Uri initialURL = new Uri("https://api.stormglass.io/v2/weather/point");
            string allParameters = String.Format("?lat={0}&lng={1}&params={2}&start={3}&end={4}&source={5}",
                latitude, longitude, parameters, timeStamp, timeStamp, source);
            Uri finalURL = new Uri(initialURL, allParameters);
            webHelper.RequestURL = finalURL;
        }
    }
}
