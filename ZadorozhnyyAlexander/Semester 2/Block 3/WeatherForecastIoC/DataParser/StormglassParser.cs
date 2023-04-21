using AbstractWeatherForecast;
using RequestApi;
using Newtonsoft.Json;


namespace DataParsers
{
    public class StormglassParser : AParser
    {
        public StormglassParser(ApiHelper apiHelper) : base(apiHelper) { }

        protected string? GetValueFromJson(IList<IDictionary<string, object>>? arr, string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<IDictionary<string, float>>(arr[0][value].ToString())["sg"].ToString();
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

                var rightArrayFromJson = (JsonConvert.DeserializeObject<IList<IDictionary<string, object>>>
                    (JsonConvert.DeserializeObject<IDictionary<string, object>>(result)["hours"].ToString()));

                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "airTemperature"));
                if (resultJsonValues[0] != "No data")
                    resultJsonValues.Add((float.Parse(resultJsonValues[0]) * 1.8f + 32f).ToString());
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "cloudCover"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "precipitation"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "humidity"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "windDirection"));
                resultJsonValues.Add(GetValueFromJson(rightArrayFromJson, "windSpeed"));

                return resultJsonValues;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}