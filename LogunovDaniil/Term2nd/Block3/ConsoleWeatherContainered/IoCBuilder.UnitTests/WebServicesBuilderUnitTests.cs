using NUnit.Framework;
using WebWeatherRequester;
using WeatherRequesterResourceLibrary;
using Moq;

namespace IoCBuilder.UnitTests
{
	public class Tests
	{
		private RequestDataContainer NormalRequest = new RequestDataContainer
		{
			ServiceURL = "https://google.com"
		};
		private Mock<AWebServiceAPI> mock1 = new Mock<AWebServiceAPI>("I'm just a regular everyday");
		private Mock<AWebServiceAPI> mock2 = new Mock<AWebServiceAPI>("normal mothe...");

		[SetUp]
		public void CreateMockObjects()
		{
			mock1.Setup(x => x.GetServiceRequestInfo()).Returns(NormalRequest);
			mock1.Setup(x => x.ParseJSONObject(It.IsAny<string>())).Returns(new WeatherDataContainer());

			mock2.Setup(x => x.GetServiceRequestInfo()).Returns(NormalRequest);
			mock2.Setup(x => x.ParseJSONObject(It.IsAny<string>())).Returns(new WeatherDataContainer());
		}

		[Test]
		public void AddWebServiceUnitTest()
		{
			var builder = new WebServicesBuilder();
			builder.AddProvidedWebAPI(mock1.Object, "one");
			builder.AddProvidedWebAPI(mock2.Object, "two");

			Assert.True(builder.IncludeWebService("one"));
			Assert.True(builder.IncludeWebService("two"));
			Assert.False(builder.IncludeWebService("three"));
			Assert.False(builder.IncludeWebService("two"));
		}

		[Test]
		public void RemoveWebServiceUnitTest()
		{
			var builder = new WebServicesBuilder();
			builder.AddProvidedWebAPI(mock1.Object, "one");
			builder.AddProvidedWebAPI(mock2.Object, "two");

			builder.IncludeWebService("one");
			builder.IncludeWebService("two");
			
			Assert.True(builder.ExcludeWebService("one"));
			Assert.True(builder.ExcludeWebService("two"));
			Assert.False(builder.ExcludeWebService("one"));
			Assert.False(builder.ExcludeWebService("three"));
		}

		[Test]
		public void RunWebServiceUnitTest()
		{
			var builder = new WebServicesBuilder();
			builder.AddProvidedWebAPI(mock1.Object, "one");
			builder.AddProvidedWebAPI(mock2.Object, "two");

			builder.IncludeWebService("one");
			builder.IncludeWebService("two");
			builder.ExcludeWebService("one");

			var requesters = builder.GetWeatherRequesters();
			foreach (var requester in requesters)
			{
				requester.FetchWeatherData();
			}

			mock1.Verify(mock => mock.ParseJSONObject(It.IsAny<string>()), Times.Never());
			mock2.Verify(mock => mock.ParseJSONObject(It.IsAny<string>()), Times.Once());
		}
	}
}