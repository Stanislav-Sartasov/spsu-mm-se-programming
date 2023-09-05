using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Drawing;

namespace WebLibrary
{
    public class GetRequest : IGetRequest
    {
        private string address;
        private string[] headers;
        private string responseAsString;
        private HttpWebRequest request;
        public GetRequest(string path, IReadOnlyList<string> additionalHeaders)
        {
            if (path != null)
                address = path;
            else
                address = "";

            if (additionalHeaders != null)
                foreach (var header in additionalHeaders)
                    headers = header.Split(":");
        }

        public string Send()
        {
            try
            {
                request = (HttpWebRequest)WebRequest.Create(address);
                request.Method = "Get";

                if (headers != null) request.Headers.Add(headers[0], headers[1]);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream stream = response.GetResponseStream();

                if (stream != null)
                {
                    responseAsString = new StreamReader(stream).ReadToEnd();
                }

                return "AllFine";
            }
            catch (global::System.Exception exc)
            {
                return exc.Message;
            }  
        }
        public string GetResponce()
        {
            return responseAsString;
        }
    }
}
