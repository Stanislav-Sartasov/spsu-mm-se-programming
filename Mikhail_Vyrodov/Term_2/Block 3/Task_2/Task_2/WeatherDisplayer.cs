using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class WeatherDisplayer
    {
        public StormglassioParametersFiller StormglassioParamsFiller { get; private set; }
        public TomorrowioParametersFiller TomorrowioParamsFiller { get; private set; }
        public string Answer { get; private set; } = "";

        private ConsoleWriter writer;
        private IWebServerHelper tomorrowioWebHelper;
        private IWebServerHelper stormglassioWebHelper;

        public WeatherDisplayer(IWebServerHelper tomorrowioHelper, IWebServerHelper stormglassioHelper)
        {
            StormglassioParamsFiller = new StormglassioParametersFiller();
            TomorrowioParamsFiller = new TomorrowioParametersFiller();
            writer = new ConsoleWriter();
            this.tomorrowioWebHelper = tomorrowioHelper;
            this.stormglassioWebHelper = stormglassioHelper;
        }

        public void DisplayTomorrowioWeather(IResponseReader respReader)
        {
            if (TomorrowioParamsFiller.FillParameters(tomorrowioWebHelper, respReader))
            {
                writer.Parameters = TomorrowioParamsFiller.Parameters;
                Answer += writer.ShowSiteWeather();
            }
        }

        public void DisplayStormglassioWeather(IResponseReader respReader)
        {
            if (StormglassioParamsFiller.FillParameters(stormglassioWebHelper, respReader))
            {
                writer.Parameters = StormglassioParamsFiller.Parameters;
                Answer += writer.ShowSiteWeather();
            }
        }
    }
}
