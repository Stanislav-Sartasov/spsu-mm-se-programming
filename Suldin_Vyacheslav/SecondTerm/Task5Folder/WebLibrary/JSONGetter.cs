using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace WebLibrary
{
    public class JsonGetter
    {
        private JObject json;
        private IGetRequest gr;
        public JsonGetter(IGetRequest getRequest)
        {
            gr = getRequest;
        }

        public virtual JObject GetJSON()
        {
            string sendState = gr.Send();

            var response = gr.GetResponce();
            if (response != null)
            {
                json = JObject.Parse(response);
            }
            else json = JObject.Parse("{\"ERROR\":\"" + sendState + "\"}");

            return this.json;
        }
    }
}
