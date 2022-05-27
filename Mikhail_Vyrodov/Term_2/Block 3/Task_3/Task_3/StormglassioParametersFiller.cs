using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace Task_3
{
    public class StormglassioParametersFiller : AParametersFiller, IParametersFiller<StormglassDataHolder>
    {
        public IJsonMapper<StormglassDataHolder> JsonMapper { get; private set; }

        public StormglassioParametersFiller(IRequestURLFiller requestFiller, IJsonMapper<StormglassDataHolder> jsonMapper)
        {
            RequestURLFiller = requestFiller;
            JsonMapper = jsonMapper;
        }

        protected override void ExtractParameters(string json)
        {
            StormglassDataHolder dataHolder = JsonConvert.DeserializeObject<StormglassDataHolder>(json);
            Parameters = JsonMapper.GetParameters(dataHolder);
        }
    }
}
