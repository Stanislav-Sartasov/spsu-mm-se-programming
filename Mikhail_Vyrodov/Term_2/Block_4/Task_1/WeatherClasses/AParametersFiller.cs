using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace WeatherClasses
{
    public abstract class AParametersFiller
    {
        public IRequestURLFiller RequestURLFiller { get; protected set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Answer { get; private set; }

        public bool FillParameters(IWebServerHelper webHelper, IResponseReader respReader)
        {
            RequestURLFiller.FillRequestURL(webHelper);
            if (webHelper.MakeRequest())
            {
                respReader.Response = webHelper.Response;
                string json = respReader.GetResponseInfo();
                ExtractParameters(json);
                return true;
            }
            Answer = webHelper.Answer;
            return false;
        }

        protected abstract void ExtractParameters(string json);
    }
}
