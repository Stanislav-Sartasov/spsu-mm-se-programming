using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherForecastModel;

namespace WeatherAPI
{
	public class TomorrowAPI : AWeatherAPI
	{
		public TomorrowAPI() : base("https://api.tomorrow.io/v4/timelines?location=59.93863%2C%2030.31413&fields=temperature&fields=humidity&fields=windSpeed&fields=windDirection&fields=precipitationProbability&fields=cloudCover&units=metric&timesteps=current&timezone=Europe%2FMoscow&apikey=") { }

		public override async Task<string> GetDataAsync()
		{
			try
			{
				Client.DefaultRequestHeaders.Authorization = null;
				HttpResponseMessage responseHttp = await Client.GetAsync(URL + Keys.TomorrowIoAPIkey);
				responseHttp.EnsureSuccessStatusCode();

				return await responseHttp.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				flag = false;
				throw e;
			}
		}

		public override WeatherModel GetWeatherModelAsync(string responseString)
		{
			JObject json = JObject.Parse(responseString);
			JObject? values = (JObject?)json["data"]["timelines"][0]["intervals"][0]["values"];

			WeatherModel? model = JsonConvert.DeserializeObject<WeatherModel>(values.ToString());
			return model;
		}
	}
}