using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClasses
{
    public class StormglassParametersProvider : AParametersProvider
    {
        public StormglassParametersProvider(IResponseReader respReader, IWebServerHelper webHelper)
        {
            Weather = new WeatherCharacterization();
            this.webHelper = webHelper;
            this.respReader = respReader;
            paramsFiller = new StormglassioParametersFiller();
        }
    }
}
