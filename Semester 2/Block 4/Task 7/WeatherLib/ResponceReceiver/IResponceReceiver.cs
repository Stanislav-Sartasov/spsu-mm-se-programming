using System.Threading.Tasks;

namespace WeatherLib.ResponceReceiver
{
	public interface IResponceReceiver
	{
		bool IsSucceed { get; }
		string Responce { get; }
		public Task GetResponce(string url);
	}
}
