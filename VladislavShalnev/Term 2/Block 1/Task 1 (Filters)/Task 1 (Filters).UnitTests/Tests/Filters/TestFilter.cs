using NUnit.Framework;
using Core.Image;
using Core.Filter.Interfaces;

namespace Filters.UnitTests
{
	public static class TestFilter
	{
		public static void Test(IFilter filter, string input, string output)
		{
			Bitmap bitmap = new Bitmap(input);

			filter.ApplyTo(bitmap);

			bitmap.Save(output);
		}
	}
}