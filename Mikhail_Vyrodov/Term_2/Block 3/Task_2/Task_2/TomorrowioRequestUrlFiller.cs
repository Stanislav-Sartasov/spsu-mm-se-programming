using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class TomorrowioRequestURLFiller : IRequestURLFiller
    {
        private const string latitude = "59.57";
        private const string longitude = "30.19";
        public void FillRequestURL(IWebServerHelper webHelper)
        {
            Uri initialURL = new Uri("https://api.tomorrow.io/v4/timelines?");
            string fields = "temperature,cloudCover,humidity,precipitationIntensity,windDirection,windSpeed";
            string apiKey = "AVMNJMtSlSrsgXtt1gIB6x2MrgKqIqxO";
            string allParameters = String.Format("?location={0},{1}&fields={2}&timesteps=current&units=metric&apikey={3}",
                latitude, longitude, fields, apiKey);
            Uri finalURL = new Uri(initialURL, allParameters);
            webHelper.RequestURL = finalURL;
        }
    }
}
