using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WeatherClasses
{
    public class TomorrowParametersProvider : AParametersProvider
    {

        public TomorrowParametersProvider(IResponseReader respReader, IWebServerHelper webHelper)
        {
            Weather = new WeatherCharacterization();
            this.webHelper = webHelper;
            this.respReader = respReader;
            paramsFiller = new TomorrowioParametersFiller();
        }
    }
}
