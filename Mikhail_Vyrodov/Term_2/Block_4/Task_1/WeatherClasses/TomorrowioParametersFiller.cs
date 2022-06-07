using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace WeatherClasses
{
    public class TomorrowioParametersFiller : AParametersFiller, IParametersFiller<TomorrowDataHolder>
    {
        public IJsonMapper<TomorrowDataHolder> JsonMapper { get; private set; }

        public TomorrowioParametersFiller()
        {
            RequestURLFiller = new TomorrowioRequestURLFiller();
            JsonMapper = new TomorrowioMapper();
        }

        protected override void ExtractParameters(string json)
        {
            TomorrowDataHolder dataHolder = JsonConvert.DeserializeObject<TomorrowDataHolder>(json);
            Parameters = JsonMapper.GetParameters(dataHolder);
        }
    }
}
