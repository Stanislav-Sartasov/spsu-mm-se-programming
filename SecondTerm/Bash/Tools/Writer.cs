using System;

namespace Tools
{
	public class Writer : IWriter
	{
		public void Write(string output)
		{
			Console.WriteLine(output);
		}
	}
}
