using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;

namespace Task_3
{
    public class StormglassioWeatherDisplayer : IWeatherDisplayer
    {
        public IParametersFiller<StormglassDataHolder> ParamsFiller { get; private set; }
        public string Answer { get; private set; } = "";
        public ConsoleWriter Writer { get; private set; }
        public IWebServerHelper WebHelper { get; private set; }
        public IResponseReader RespReader { get; private set; }

        public StormglassioWeatherDisplayer(IWebServerHelper stormglassioHelper, IParametersFiller<StormglassDataHolder> stormglassioParamsFiller,
            IResponseReader respReader, ConsoleWriter writer)
        {
            ParamsFiller = stormglassioParamsFiller;
            Writer = writer;
            WebHelper = stormglassioHelper;
            RespReader = respReader;
        }

        public bool DisplayWeather()
        {
            if (ParamsFiller.FillParameters(WebHelper, RespReader))
            {
                Writer.Parameters = ParamsFiller.Parameters;
                Answer += Writer.ShowSiteWeather();
                return true;
            }
            return false;
        }
    }
}
