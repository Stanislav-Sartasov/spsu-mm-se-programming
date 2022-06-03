using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace WeatherClasses
{
    public class ResponseReader : IResponseReader
    {
        public HttpWebResponse Response { get; set; }

        public string GetResponseInfo()
        {
            Stream stream = Response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string info = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            Response.Close();
            return info;
        }
    }
}
