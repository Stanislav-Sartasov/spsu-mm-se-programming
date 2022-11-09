using System;

namespace Tools
{
	public class Reader : IReader
	{
		public string Read()
		{
			var str = Console.ReadLine();
			if (str == null)
				return "";
			return str;
		}
	}
}
