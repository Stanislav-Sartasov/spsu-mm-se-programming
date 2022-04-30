using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIoC
{
	public abstract class WeatherParser : IWeatherParser
	{
		private IWebParser _webParser;

		protected string _url;
		protected string _apiKey;
		protected string name;
		public string Name => name;

		protected WeatherParser(IWebParser webParser)
		{
			_webParser = webParser;
		}

		public WeatherData CollectData()
		{
			string dataJson = _webParser.GetData(_url + _apiKey);

			WeatherData weatherData = FillWeatherData(dataJson);

			if (weatherData.IsNotEmpty())
				return weatherData;
			else
				throw new EmptyWeatherDataException("Website gave bad responce and weather data is not filled properly");
		}

		protected abstract WeatherData FillWeatherData(string dataJson);
	}
}
