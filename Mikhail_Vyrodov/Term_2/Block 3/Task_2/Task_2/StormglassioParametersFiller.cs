using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace Task_2
{
    public class StormglassioParametersFiller : IParametersFiller
    {
        private StormglassioRequestURLFiller stormglassRequestFiller;
        private StormglassioMapper stormglassMapper;

        public Dictionary<string, string> Parameters { get; private set; }

        public StormglassioParametersFiller()
        {
            stormglassRequestFiller = new StormglassioRequestURLFiller();
            stormglassMapper = new StormglassioMapper();
        }

        public bool FillParameters(IWebServerHelper webHelper, IResponseReader respReader)
        {
            stormglassRequestFiller.FillRequestURL(webHelper);
            if (webHelper.MakeRequest())
            {
                respReader.Response = webHelper.Response;
                string json = respReader.GetResponseInfo();
                StormGlassDataHolder stormglassDataHolder = JsonConvert.DeserializeObject<StormGlassDataHolder>(json);
                Parameters = stormglassMapper.GetParameters(stormglassDataHolder);
                return true;
            }
            return false;
        }
    }
}
