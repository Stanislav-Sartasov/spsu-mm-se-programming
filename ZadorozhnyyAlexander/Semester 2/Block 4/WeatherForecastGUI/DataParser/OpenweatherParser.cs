using AbstractWeatherForecast;
using Newtonsoft.Json;
using RequestApi;


namespace DataParsers
{
    public class OpenweatherParser : AParser
    {
        public OpenweatherParser(ApiHelper apiHelper) : base(apiHelper) { }

        protected string? GetValueFromJson(IDictionary<string, object>? dict, string category, string subcategory)
        {
            try
            {
                return JsonConvert.DeserializeObject<IDictionary<string, float>>(dict[category].ToString())[subcategory].ToString();
            }
            catch (KeyNotFoundException)
            {
                return "No data";
            }
        }

        public override List<string> GetListOfCurrentData()
        {
            HttpResponseMessage response = apiHelper.ApiClient.GetAsync(apiHelper.UrlParameters).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                var rightArrayFromJson = JsonConvert.DeserializeObject<IDictionary<string, object>>(result);

                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "main", "temp"));
                if (resultJsonValues[0] != "No data")
                    resultJsonValues.Add((float.Parse(resultJsonValues[0]) * 1.8f + 32f).ToString());
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "clouds", "all"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "main", "precipitation"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "main", "humidity"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "wind", "deg"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "wind", "speed"));

                return resultJsonValues;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
