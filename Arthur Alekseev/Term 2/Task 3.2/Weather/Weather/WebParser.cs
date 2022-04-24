using System.Net;

namespace Weather
{
	public class WebParser : IWebParser
	{
		public static WebParser Instance { get; private set; }

		public static WebParser GetInstance()
		{
			if (Instance == null)
				Instance = new WebParser();
			return Instance;
		}

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
