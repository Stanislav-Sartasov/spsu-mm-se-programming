using NUnit.Framework;
using Core.Image;
using Core.Filter.Interfaces;

namespace Filters.UnitTests
{
	public static class TestFilter
	{
		private const string WORKING_DIR = "../../../Images/";

		public static void Test(IFilter filter, string input, string output)
		{
			Bitmap bitmap = new Bitmap(WORKING_DIR + input);

			filter.ApplyTo(bitmap);

			bitmap.Save(WORKING_DIR + "output.bmp");

			FileAssert.AreEqual(WORKING_DIR + "output.bmp", WORKING_DIR + output);
		}
	}
}