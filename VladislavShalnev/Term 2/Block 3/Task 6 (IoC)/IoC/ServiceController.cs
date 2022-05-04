using Ninject;
using WeatherApi;

namespace IoC;

public class ServiceController
{
	private List<AWeatherApi> _services;
	private readonly IKernel _kernel;

	public List<AWeatherApi> Services => new(_services); // Cloning list
	
	public ServiceController()
	{
		_services = new List<AWeatherApi>();
		_kernel = new StandardKernel();
	}

	private T GetService<T>(string token) where T: AWeatherApi
	{
		_kernel.Rebind<IWeatherApi>().To<T>().WithConstructorArgument(token);
		return (T)_kernel.Get<IWeatherApi>();
	}

	public ServiceController AddService<T>(string token) where T : AWeatherApi
	{
		if (!_services.Any(service => service is T))
			_services.Add(GetService<T>(token));
		
		return this;
	}

	public ServiceController RemoveService<T>() where T : AWeatherApi
	{
		_services = _services.Where(service => service is not T).ToList();
		
		return this;
	}
}