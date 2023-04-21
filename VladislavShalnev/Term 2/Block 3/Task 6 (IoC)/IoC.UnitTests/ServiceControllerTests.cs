using NUnit.Framework;
using OpenWeatherMapApi;
using TomorrowIoApi;

namespace IoC.UnitTests;

public class Tests
{
	private ServiceController _serviceController;
	
	[SetUp]
	public void Setup()
	{
		_serviceController = new ServiceController();
	}

	[Test]
	public void GetServicesListTest()
	{
		Assert.AreEqual(0, _serviceController.Services.Count);
		
		_serviceController.Services.Add(new TomorrowIo("token"));
		
		Assert.AreEqual(0, _serviceController.Services.Count);
	}
	
	[Test]
	public void AddServiceTest()
	{
		_serviceController.AddService<TomorrowIo>("token");
		_serviceController.AddService<OpenWeatherMap>("token");
		Assert.AreEqual(2, _serviceController.Services.Count);
		Assert.AreEqual(typeof(TomorrowIo), _serviceController.Services[0].GetType());
		Assert.AreEqual(typeof(OpenWeatherMap), _serviceController.Services[1].GetType());
		
		// Trying to add same service
		_serviceController.AddService<TomorrowIo>("token");
		Assert.AreEqual(2, _serviceController.Services.Count);
	}
	
	[Test]
	public void RemoveServiceTest()
	{
		_serviceController.AddService<TomorrowIo>("token");
		_serviceController.AddService<OpenWeatherMap>("token");
		
		_serviceController.RemoveService<OpenWeatherMap>();
		Assert.AreEqual(1, _serviceController.Services.Count);
		
		// Trying to remove same service
		_serviceController.RemoveService<OpenWeatherMap>();
		Assert.AreEqual(1, _serviceController.Services.Count);
	}
}