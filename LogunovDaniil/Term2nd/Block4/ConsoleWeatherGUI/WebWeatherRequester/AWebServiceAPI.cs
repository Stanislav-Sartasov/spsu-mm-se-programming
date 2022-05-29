using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherRequesterResourceLibrary;

namespace WebWeatherRequester
{
	public abstract class AWebServiceAPI : IWebServiceHandler
	{
		protected string apiKey;

		public AWebServiceAPI(string key)
		{
			apiKey = key;
		}

		public abstract RequestDataContainer GetServiceRequestInfo();

		public abstract WeatherDataContainer? ParseJSONObject(string json);
	}
}
