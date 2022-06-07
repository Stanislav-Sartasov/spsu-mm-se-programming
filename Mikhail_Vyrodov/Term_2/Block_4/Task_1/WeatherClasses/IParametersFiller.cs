using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;

namespace WeatherClasses
{
    public interface IParametersFiller<T>
    {
        public IRequestURLFiller RequestURLFiller { get; }
        public IJsonMapper<T> JsonMapper { get; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Answer { get; }

        public bool FillParameters(IWebServerHelper webHelper, IResponseReader respReader);

    }
}
