using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace WebLibrary
{
    public class JsonHolder : IJsonHolder
    {
        public JObject Json { get; private set; }
        private IGetRequest gr;
        public JsonHolder(IGetRequest getRequest)
        {
            gr = getRequest;
            Json = GetJSON();
        }

        public JObject GetJSON()
        {
            string sendState = gr.Send();

            var response = gr.GetResponce();
            if (response != null)
            {
                Json = JObject.Parse(response);
            }
            else Json = JObject.Parse("{\"ERROR\":\"" + sendState + "\"}");

            return this.Json;
        }
    }
}
