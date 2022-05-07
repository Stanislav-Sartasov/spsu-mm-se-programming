using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace Task_2
{
    public class TomorrowioParametersFiller : IParametersFiller
    {
        private TomorrowioRequestURLFiller tomorrowRequestFiller;
        private TomorrowioMapper tomorrowMapper;

        public Dictionary<string, string> Parameters { get; private set; }

        public TomorrowioParametersFiller()
        {
            tomorrowRequestFiller = new TomorrowioRequestURLFiller();
            tomorrowMapper = new TomorrowioMapper();
        }

        public bool FillParameters(IWebServerHelper webHelper, IResponseReader respReader)
        {
            tomorrowRequestFiller.FillRequestURL(webHelper);
            if (webHelper.MakeRequest())
            {
                respReader.Response = webHelper.Response;
                string json = respReader.GetResponseInfo();
                TomorrowDataHolder tomorrowDataHolder = JsonConvert.DeserializeObject<TomorrowDataHolder>(json);
                Parameters = tomorrowMapper.GetParameters(tomorrowDataHolder);
                return true;
            }
            return false;
        }
    }
}
