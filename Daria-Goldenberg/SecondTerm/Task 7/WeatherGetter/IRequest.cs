namespace WeatherGetter
{
	public interface IRequest
	{
		public string Response { get; }
		public bool Connected { get; }

		public void Run(string address);
	}
}