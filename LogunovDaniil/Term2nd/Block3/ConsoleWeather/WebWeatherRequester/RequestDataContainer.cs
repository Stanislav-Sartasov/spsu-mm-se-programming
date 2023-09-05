namespace WebWeatherRequester
{
	public class RequestDataContainer
	{
		public string ServiceURL { get; init; }
		public IReadOnlyCollection<string> ParamKeys { get; init; } = new List<string>();
		public IReadOnlyCollection<string> ParamValues { get; init; } = new List<string>();
		public IReadOnlyCollection<string> HeaderKeys { get; init; } = new List<string>();
		public IReadOnlyCollection<string> HeaderValues { get; init; } = new List<string>();
	}
}
