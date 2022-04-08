using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;
using Parsers;
using System.Text.RegularExpressions;

namespace Task5
{
	public class ConsoleWriter
	{
		public static void WtireLines(IReadOnlyList<string> lines)
		{
			if (lines == null)
            {
				return;
            }
			Console.WriteLine("\n");

			try
			{
				var trigger = lines[1];
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.Write( lines[0].Split(".")[1] + ": " + (ErrorType)Convert.ToInt32(Regex.Replace(lines[0].Split(".")[0], @"[^\d]+", "")));
				return;
			}

			foreach (var line in lines)
            {
                try
                {
                    Console.Write(line);
                    Fill(line.Length);
                }
                catch (NullReferenceException)
                {
					return;
                }
            }
        }
		public static void Fill(int n)
		{
			for (int i = 0; i < (17 - n); i++)
				Console.Write(" ");
		}
	}
}
