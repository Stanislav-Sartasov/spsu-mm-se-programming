using Microsoft.VisualStudio.TestTools.UnitTesting;
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

		[TestMethod]
		public void CheckTest()
		{
			string[] testArgs = { "MyInstagram", "C:\\Users\\Mikle\\source\\repos\\SecondTerm\\Filters\\panda.bmp",
				"Averaging", "C:\\Users\\Mikle\\source\\repos\\SecondTerm\\Filters\\out.bmp" };

			Assert.IsTrue(InputCheck.Check(testArgs));

			string[] wrongeAmountArgs = { "", "" };
			string[] wrongSecondArgs = { "MyInstagram", "C:\\Users\\Mikle\\source\\repos\\SecondTerm\\Filters\\false.bmp",
				"Averaging", "C:\\Users\\Mikle\\source\\repos\\SecondTerm\\Filters\\out.bmp" };
			string[] wrongFilterArgs = { "MyInstagram", "C:\\Users\\Mikle\\source\\repos\\SecondTerm\\Filters\\panda.bmp",
				"WrongFilter", "C:\\Users\\Mikle\\source\\repos\\SecondTerm\\Filters\\out.bmp" };

			Assert.IsFalse(InputCheck.Check(wrongeAmountArgs));
			Assert.IsFalse(InputCheck.Check(wrongSecondArgs));
			Assert.IsFalse(InputCheck.Check(wrongFilterArgs));
		}
	}
}