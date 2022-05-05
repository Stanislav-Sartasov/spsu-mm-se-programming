using WeatherRequesterResourceLibrary;
using System.Net;

namespace WebWeatherRequester
{
	public interface IWebServiceHandler
	{
		public RequestDataContainer GetServiceRequestInfo();

		public WeatherDataContainer? ParseJSONObject(string json);
	}
}
