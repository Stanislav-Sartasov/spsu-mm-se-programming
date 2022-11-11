using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
			foreach (string filter in InputCheck.Filters)
			{
				string path = Directory.GetCurrentDirectory() + @"..\..\..\..\Images\";
				expected.Read(path + filter + ".bmp");
				args[1] = path + "panda.bmp";
				args[2] = filter;
				args[3] = path + $"out{filter}.bmp";
				actual.ImageProcessing(args);
				foreach (byte i in expected.mas)
				{
					Assert.AreEqual(expected.mas[i], actual.mas[i]);
				}
				File.Delete(path + $"out{filter}.bmp");
			}
		}

		[TestMethod]
		public void CheckTest()
		{
			string path = Directory.GetCurrentDirectory() + @"..\..\..\..\Images\";


			string[] testArgs = { "MyInstagram", path + "panda.bmp",
				"Averaging", path + "out.bmp" };

			Assert.IsTrue(InputCheck.Check(testArgs));

			string[] wrongeAmountArgs = { "", "" };
			string[] wrongSecondArgs = { "MyInstagram", path + "wrongPanda.bmp",
				"Averaging", path + "out.bmp" };
			string[] wrongFilterArgs = { "MyInstagram", path + "panda.bmp",
				"wrongFilter", path + "out.bmp" };

			Assert.IsFalse(InputCheck.Check(wrongeAmountArgs));
			Assert.IsFalse(InputCheck.Check(wrongSecondArgs));
			Assert.IsFalse(InputCheck.Check(wrongFilterArgs));
		}
	}
}