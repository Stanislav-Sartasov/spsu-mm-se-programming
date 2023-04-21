using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WeatherClasses
{
    public interface IResponseReader
    {
        public HttpWebResponse Response { get; set; }

        public string GetResponseInfo();
    }
}
