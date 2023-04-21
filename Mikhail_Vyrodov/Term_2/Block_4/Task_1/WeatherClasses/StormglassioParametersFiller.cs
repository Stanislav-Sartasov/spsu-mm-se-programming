using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace WeatherClasses
{
    public class StormglassioParametersFiller : AParametersFiller, IParametersFiller<StormglassDataHolder>
    {
        public IJsonMapper<StormglassDataHolder> JsonMapper { get; private set; }

        public StormglassioParametersFiller()
        {
            RequestURLFiller = new StormglassioRequestURLFiller();
            JsonMapper = new StormglassioMapper();
        }

        protected override void ExtractParameters(string json)
        {
            StormglassDataHolder dataHolder = JsonConvert.DeserializeObject<StormglassDataHolder>(json);
            Parameters = JsonMapper.GetParameters(dataHolder);
        }
    }
}
