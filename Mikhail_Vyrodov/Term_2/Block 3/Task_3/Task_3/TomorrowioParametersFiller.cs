using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace Task_3
{
    public class TomorrowioParametersFiller : AParametersFiller, IParametersFiller<TomorrowDataHolder>
    {
        public IJsonMapper<TomorrowDataHolder> JsonMapper { get; private set; }

        public TomorrowioParametersFiller(IRequestURLFiller requestFiller, IJsonMapper<TomorrowDataHolder> jsonMapper)
        {
            RequestURLFiller = requestFiller;
            JsonMapper = jsonMapper;
        }

        protected override void ExtractParameters(string json)
        {
            TomorrowDataHolder dataHolder = JsonConvert.DeserializeObject<TomorrowDataHolder>(json);
            Parameters = JsonMapper.GetParameters(dataHolder);
        }
    }
}
