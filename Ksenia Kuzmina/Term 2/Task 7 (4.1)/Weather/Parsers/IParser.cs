using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Parsers
{
	public interface IParser
	{
		public string Name { get; }

		public abstract Task<Weather> GetWeatherInfoAsync();
	}
}
