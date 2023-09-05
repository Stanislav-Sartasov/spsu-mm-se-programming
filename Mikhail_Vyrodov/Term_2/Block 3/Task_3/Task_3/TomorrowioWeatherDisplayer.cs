using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;

namespace Task_3
{
    public class TomorrowioWeatherDisplayer : IWeatherDisplayer
    {
        public IParametersFiller<TomorrowDataHolder> ParamsFiller { get; private set; }
        public string Answer { get; private set; } = "";
        public ConsoleWriter Writer { get; private set; }
        public IWebServerHelper WebHelper { get; private set; }
        public IResponseReader RespReader { get; private set; }

        public TomorrowioWeatherDisplayer(IWebServerHelper tomorrowioHelper, IParametersFiller<TomorrowDataHolder> tomorrowioParamsFiller,
            IResponseReader respReader, ConsoleWriter writer)
        {
            ParamsFiller = tomorrowioParamsFiller;
            Writer = writer;
            WebHelper = tomorrowioHelper;
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
