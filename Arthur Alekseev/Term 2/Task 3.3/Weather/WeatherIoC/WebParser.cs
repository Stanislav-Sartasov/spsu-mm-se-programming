using System.Net;

namespace WeatherIoC
{
	public class WebParser : IWebParser
	{
		public WebParser() { }

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
