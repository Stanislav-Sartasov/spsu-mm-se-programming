using System;

namespace Filters
{
	class Program
	{
		static void Main(string[] args)
		{
			if (InputCheck.Check(args))
			{
				new Image().ImageProcessing(args);
			}
		}
	}
}
