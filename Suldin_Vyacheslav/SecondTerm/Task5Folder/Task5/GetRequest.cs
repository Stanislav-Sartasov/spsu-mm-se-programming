using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
namespace Task5
{
    public class GetRequest
    {
        HttpWebRequest Request;

        HttpRequestHeaders Headers;

        public string ResponseAsString;
        public GetRequest(string address)
        {
            Request = (HttpWebRequest)WebRequest.Create(address);
            Request.Method = "Get";
        }
        public GetRequest(string address, string[] headers)
        {

            Request = (HttpWebRequest)WebRequest.Create(address);
            Request.Method = "Get";

            foreach (string header in headers)
            {
                string[] splittedHeader = header.Split(":");
                Request.Headers.Add(splittedHeader[0], splittedHeader[1]);
            }
            
            
        }


        public void Start()
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();

                Stream stream = response.GetResponseStream();

                if (stream != null)
                {
                    ResponseAsString = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (global::System.Exception)
            {

                throw;
            }  
        }
    }
}
