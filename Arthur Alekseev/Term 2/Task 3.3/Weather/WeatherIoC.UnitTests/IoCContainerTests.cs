using NUnit.Framework;

namespace WeatherIoC.UnitTests
{
	public class IoCContainerTests
	{

		[Test]
		public void IoCContainerReturnNoServices()
		{
			var container = new IoCContainer();
			bool generateTomorrowIo = false;
			bool generateOpenWeatherMap = false;

			Assert.AreEqual(0, container.CreateParsers(generateOpenWeatherMap, generateTomorrowIo).Count);
		}

		[Test]
		public void IoCContainerReturnBothServices()
		{
			var container = new IoCContainer();
			bool generateTomorrowIo = true;
			bool generateOpenWeatherMap = true;

			var parsers = container.CreateParsers(generateOpenWeatherMap, generateTomorrowIo);

			Assert.AreEqual(2, parsers.Count);
			Assert.IsTrue(parsers[0] is OpenWeatherMapParser);
			Assert.IsTrue(parsers[1] is TomorrowIoParser);
		}

		[Test]
		public void IoCContainerReturnTomorrowio()
		{
			var container = new IoCContainer();
			bool generateTomorrowIo = true;
			bool generateOpenWeatherMap = false;

			var parsers = container.CreateParsers(generateOpenWeatherMap, generateTomorrowIo);

			Assert.AreEqual(1, parsers.Count);
			Assert.IsTrue(parsers[0] is TomorrowIoParser);
		}

		[Test]
		public void IoCContainerReturnOpenWeatherMap()
		{
			var container = new IoCContainer();
			bool generateTomorrowIo = false;
			bool generateOpenWeatherMap = true;

			var parsers = container.CreateParsers(generateOpenWeatherMap, generateTomorrowIo);

			Assert.AreEqual(1, parsers.Count);
			Assert.IsTrue(parsers[0] is OpenWeatherMapParser);
		}
	}
}
