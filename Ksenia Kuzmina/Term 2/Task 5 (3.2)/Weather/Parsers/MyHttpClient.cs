using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Parsers
{
	public class MyHttpClient : IHttpClient
	{
		private HttpClient _httpClient = new HttpClient();

		public async Task<string> GetData(string url)
		{
			return await (await _httpClient.GetAsync(url)).Content.ReadAsStringAsync();
		}
	}
}
