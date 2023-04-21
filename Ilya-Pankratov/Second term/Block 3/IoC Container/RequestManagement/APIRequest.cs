using System.Net;

namespace RequestManagement
{
    public class APIRequest : IRequest
    {
        public string Response { get; private set; }
        public bool Connected { get; private set; }
        private HttpWebRequest request;
        private string address;

        public APIRequest(string address)
        {
            this.address = address;
            request = null;
            Response = null;
        }

        public string Get()
        {
            try
            {
                request = (HttpWebRequest)WebRequest.Create(address);
            }
            catch (Exception e)
            {
                Connected = false;
                Response = "Invalid link";
                return Response;
            }
            
            request.Method = "Get";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream != null)
                {
                    Response = new StreamReader(stream).ReadToEnd();
                }

                Connected = true;
            }
            catch(Exception ex)
            {
                Response = "Site does not response";
                Connected = false;
            }

            return Response;
        }
    }
}
