using Task_1.Core.Image;
using Task_1.Core.Filter.Interfaces;

namespace Task_1.UnitTests.Filters
{
	public static class TestFilter
	{
		public static void Test(IFilter filter, string input, string output)
		{
			Bitmap bitmap = new Bitmap(input);

			bitmap = filter.ApplyTo(bitmap);

			bitmap.Save(output);
		}
	}
}