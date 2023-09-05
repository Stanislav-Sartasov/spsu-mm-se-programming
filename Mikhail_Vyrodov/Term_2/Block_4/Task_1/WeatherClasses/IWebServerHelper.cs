using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WeatherClasses
{
    public interface IWebServerHelper
    {
        public bool MakeRequest();

        public Uri RequestURL { get; set; }
        public HttpWebResponse Response { get; }
        public string Answer { get; }
    }
}
