using NUnit.Framework;

namespace WeatherIoC.UnitTests
{
	public class IoCContainerTests
	{

		[Test]
		public void IoCContainerReturnNoServices()
		{
			var container = new IoCContainer();
			container.GenerateTomorrowIo = false;
			container.GenerateOpenWeatherMap = false;

			Assert.AreEqual(0, container.CreateParsers().Count);
		}

		[Test]
		public void IoCContainerReturnBothServices()
		{
			var container = new IoCContainer();
			container.GenerateTomorrowIo = true;
			container.GenerateOpenWeatherMap = true;

			var parsers = container.CreateParsers();

			Assert.AreEqual(2, parsers.Count);
			Assert.IsTrue(parsers[0] is OpenWeatherMapParser);
			Assert.IsTrue(parsers[1] is TomorrowIoParser);
		}

		[Test]
		public void IoCContainerReturnTomorrowio()
		{
			var container = new IoCContainer();
			container.GenerateTomorrowIo = true;
			container.GenerateOpenWeatherMap = false;

			var parsers = container.CreateParsers();

			Assert.AreEqual(1, parsers.Count);
			Assert.IsTrue(parsers[0] is TomorrowIoParser);
		}

		[Test]
		public void IoCContainerReturnOpenWeatherMap()
		{
			var container = new IoCContainer();
			container.GenerateTomorrowIo = false;
			container.GenerateOpenWeatherMap = true;

			var parsers = container.CreateParsers();

			Assert.AreEqual(1, parsers.Count);
			Assert.IsTrue(parsers[0] is OpenWeatherMapParser);
		}
	}
}
