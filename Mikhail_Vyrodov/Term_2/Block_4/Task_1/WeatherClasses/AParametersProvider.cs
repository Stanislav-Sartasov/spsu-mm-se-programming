using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClasses
{
    public abstract class AParametersProvider
    {
        public WeatherCharacterization Weather { get; protected set; }

        protected IWebServerHelper webHelper;
        protected IResponseReader respReader;
        protected AParametersFiller paramsFiller;

        public void FillWeatherProperties()
        {
            paramsFiller.FillParameters(webHelper, respReader);
            Weather.FillProperties(paramsFiller.Parameters);
        }
    }
}
