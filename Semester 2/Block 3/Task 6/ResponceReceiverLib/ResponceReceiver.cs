using System.Net.Http;
using System.Threading.Tasks;

namespace ResponceReceiverLib
{
	public class ResponceReceiver : IResponceReceiver
	{
		public bool IsSucceed { get; private set; }
		public string Responce { get; private set; }

		public ResponceReceiver(string url)
		{
			var task = GetResponce(url);
			Task.WaitAll(task);
		}

		private async Task GetResponce(string url)
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
