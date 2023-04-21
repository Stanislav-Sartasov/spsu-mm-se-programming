using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherLib.ResponceReceiver
{
	public class ResponceReceiver : IResponceReceiver
	{
		public bool IsSucceed { get; private set; }
		public string Responce { get; private set; }

		public async Task GetResponce(string url)
		{
			HttpClient client = new HttpClient();
			try
			{
				Responce = await client.GetStringAsync(url);
				IsSucceed = true;
			}
			catch
			{
				IsSucceed = false;
				Responce = null;
			}
		}
	}
}
