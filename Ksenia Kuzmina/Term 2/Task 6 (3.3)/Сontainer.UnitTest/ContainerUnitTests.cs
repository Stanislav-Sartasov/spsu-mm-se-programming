using NUnit.Framework;
using Weather.Parsers;

namespace Сontainer.UnitTests
{
	public class Tests
	{
		[Test]
		public void ContainerTest()
		{
			Container.CreateParser("TomorrowIo");
			Container.CreateParser("OpenWeather");
			Assert.Pass();
		}
	}
}