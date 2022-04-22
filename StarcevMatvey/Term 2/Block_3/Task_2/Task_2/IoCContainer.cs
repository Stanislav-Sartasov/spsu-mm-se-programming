using ISites;
using Sites;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Requests;

namespace Task_2
{
    public class IoCContainer
    {
        public IoCContainer()
        {

        }

        public List<ISite> GetSites()
        {
            Mock<IGetRequest> mock = new Mock<IGetRequest>();
            mock.Setup(x => x.Run()).Callback(() => { });
            mock.Setup(x => x.Connect).Returns(false);

            IServiceProvider service = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                    services.AddSingleton<ISite>(new OpenWeatherMap(mock.Object))
                    .AddSingleton<ISite, TomorrowIo>())
            .Build().Services;

            return service.GetServices<ISite>().ToList();
        }
    }
}
