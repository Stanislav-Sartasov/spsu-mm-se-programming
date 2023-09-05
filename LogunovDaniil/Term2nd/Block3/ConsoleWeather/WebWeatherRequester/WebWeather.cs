using WeatherRequesterResourceLibrary;
using System.Net;

namespace WebWeatherRequester
{
	public class WebWeather : IWeatherRequester
	{
		private static double RequestsDelay = 5; // min delay between two web requests, in seconds

		private DateTime lastRequestTime = DateTime.MinValue;
		private WeatherDataContainer? lastData = null;
		private FetchWeatherLog? lastLog = null;

		private IWebServiceHandler handler;

		public WebWeather(IWebServiceHandler newHandler)
		{
			handler = newHandler;
		}

		public WeatherDataContainer? FetchWeatherData()
		{
			// as to not overwhelm web services
			if ((DateTime.Now - lastRequestTime).TotalSeconds < RequestsDelay)
				return lastData;
			lastData = null;

			var request = CreateWebRequest();

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				if (response.StatusCode == HttpStatusCode.OK)
				{
					string json;
					using (Stream stream = response.GetResponseStream())
					{
						StreamReader reader = new StreamReader(stream);
						json = reader.ReadToEnd();
					}
					lastData = handler.ParseJSONObject(json);

					if (lastData != null)
						lastLog = new FetchWeatherLog
						{
							Status = FetchWeatherStatus.Success,
							Message = $"retrieved data from {handler.GetType()}"
						};
					else
						lastLog = new FetchWeatherLog
						{
							Status = FetchWeatherStatus.Error,
							Message = $"data parsing error in {handler.GetType()}"
						};
				}
				else
				{
					lastLog = new FetchWeatherLog
					{
						Status = FetchWeatherStatus.Error,
						Message = $"{handler.GetType()} returned: {response.StatusCode}"
					};
				}

				response.Close();
			}
			catch (WebException wExp)
			{
				string errorMsg;
				if (wExp.Response != null)
				{
					HttpWebResponse httpResponse = (HttpWebResponse)wExp.Response;
					errorMsg = httpResponse.StatusCode.ToString();
				}
				else
				{
					errorMsg = wExp.ToString();
				}

				lastLog = new FetchWeatherLog
				{
					Status = FetchWeatherStatus.Error,
					Message = $"{handler.GetType()} returned: {errorMsg}"
				};
			}

			lastRequestTime = DateTime.Now;

			return lastData;
		}

		public FetchWeatherLog? GetLastLog()
		{
			return lastLog;
		}

		private HttpWebRequest CreateWebRequest()
		{
			RequestDataContainer requestData = handler.GetServiceRequestInfo();
			string url = requestData.ServiceURL;
			for (int i = 0; i < Math.Min(requestData.ParamKeys.Count, requestData.ParamValues.Count); i++)
			{
				url += i == 0 ? "?" : "&";
				url += requestData.ParamKeys.ElementAt(i) + "=" + requestData.ParamValues.ElementAt(i);
			}
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			for (int i = 0; i < Math.Min(requestData.HeaderKeys.Count, requestData.HeaderValues.Count); i++)
			{
				request.Headers.Add(requestData.HeaderKeys.ElementAt(i), requestData.HeaderValues.ElementAt(i));
			}
			return request;
		}
	}
}