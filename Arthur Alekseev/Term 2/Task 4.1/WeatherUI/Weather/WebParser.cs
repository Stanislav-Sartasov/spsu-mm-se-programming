using System.Net;

namespace WeatherUI.Weather
{
	public class WebParser : IWebParser
	{
		private static WebParser? instance;
		public static WebParser Instance
		{
			get
			{
				return GetInstance();
			}
		}

		private static WebParser GetInstance()
		{
			if (instance == null)
				instance = new WebParser();
			return instance;
		}

		private WebParser() { }

		public string GetData(string address)
		{
			var request = (HttpWebRequest)WebRequest.Create(address);
			var response = (HttpWebResponse)request.GetResponse();

			string responseString;

			using (var stream = response.GetResponseStream())
			{
				using (var reader = new StreamReader(stream))
				{
					responseString = reader.ReadToEnd();
				}
			}

			return responseString;
		}
	}
}
