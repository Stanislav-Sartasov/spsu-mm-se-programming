using System;
using System.Linq;
using System.IO;

namespace Filters
{
	public static class InputCheck
	{
		public static readonly string[] Filters = { "GreyScale", "Averaging", "Negativ", "Gauss", "SobelX", "SobelY" };
		public static bool Check(string[] args)
		{
			if (args.Length != 4)
			{
				Console.WriteLine("Wrong number of arguments\n");
				return false;
			}
			if (!File.Exists(args[1]))
			{
				Console.WriteLine("Incorrect input. Invalid input file name\n");
				return false;
			}
			else if (!Filters.Contains(args[2]))
			{
				Console.WriteLine("This filter does not exist\n");
				return false;
			}
			else
				return true;
		}
	}
}
