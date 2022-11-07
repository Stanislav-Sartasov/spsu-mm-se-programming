using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filters;
using System.IO;
using System;

namespace Filters.Test
{
	[TestClass]
	public class FiltersTest
	{
		private Image expected = new();
		private Image actual = new();
		private string[] args = new string[4];
		
		[TestMethod]
		public void TestMethodUniFilter()
		{
			foreach (var filter in InputCheck.filters)
			{
				string path = Directory.GetCurrentDirectory() + @"\..\..\..\Images\" + filter + ".bmp";
				expected.Read(path);
				args[1] = path + @"\..\panda.bmp";
				args[2] = filter;
				args[3] = path + @"\..\out.bmp";
				actual.ImageProcessing(args);
				foreach (byte i in expected.mas)
				{
					Assert.AreEqual(expected.mas[i], actual.mas[i]);
				}
				File.Delete(path + @"\..\out.bmp");
			}
		}
	}
}