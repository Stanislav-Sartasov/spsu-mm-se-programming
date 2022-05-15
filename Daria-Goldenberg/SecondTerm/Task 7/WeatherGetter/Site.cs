namespace WeatherGetter
{
	public abstract class Site : ISite
	{
		protected readonly IRequest request;
		protected string apiKey = "";
		protected string url = "";
		public string Name { get; protected set; }

		public Site(IRequest request)
		{
			this.request = request;
		}

		public abstract Weather GetData();

		protected string CheckDirection(int deg)
		{
			if (23 <= deg && deg <= 67)
				return "NE";
			else if (68 <= deg && deg <= 112)
				return "E";
			else if (113 <= deg && deg <= 157)
				return "SE";
			else if (158 <= deg && deg <= 202)
				return "S";
			else if (203 <= deg && deg <= 247)
				return "SW";
			else if (248 <= deg && deg <= 292)
				return "W";
			else if (293 <= deg && deg <= 337)
				return "NW";
			return "N";
		}
	}
}
