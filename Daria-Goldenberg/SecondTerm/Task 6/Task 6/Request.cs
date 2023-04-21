using System.Net;

namespace Task_6
{
	public class Request : IRequest
	{
		private HttpWebRequest request;
		public string Response { get; private set; }
		public bool Connected { get; private set; }

		public void Run(string address)
		{
			request = (HttpWebRequest)WebRequest.Create(address);
			request.Method = "GET";

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				using (Stream stream = response.GetResponseStream())
				{
					Response = new StreamReader(stream).ReadToEnd();
				}
				Connected = true;
			}
			catch (Exception)
			{
				Connected = false;
			}
		}
	}
}