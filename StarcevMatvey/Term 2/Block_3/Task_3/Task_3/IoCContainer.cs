using ISites;
using Sites;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Requests;

namespace Task_3
{
    public class IoCContainer
    {
        private IHostBuilder builder;
        private List<SiteName> sites;
        private List<SiteName> activeSites;
        private List<SiteName> inactiveSites;

        public IoCContainer()
        {
            builder = Host.CreateDefaultBuilder();
            sites = new List<SiteName>();
            activeSites = new List<SiteName>();
            inactiveSites = new List<SiteName>();
        }

        private IoCContainer(IHostBuilder builder, List<SiteName> sites, List<SiteName> activeSites, List<SiteName> inactiveSites)
        {
            this.builder = builder;
            this.sites = new List<SiteName>(sites);
            this.activeSites = new List<SiteName>(activeSites);
            this.inactiveSites = new List<SiteName>(inactiveSites);
        }

        private IoCContainer(IoCContainer container)
        {
            builder = container.builder;
            sites = new List<SiteName>(container.sites);
            activeSites = new List<SiteName>(container.activeSites);
            inactiveSites = new List<SiteName>(container.inactiveSites);
        }

        private ISite GetSiteFromSwitcher(SiteName siteName)
        {
            switch ((int)siteName)
            {
                case 0: return new OpenWeatherMap();
                case 1: return new TomorrowIo();
                default: return null;
            }
        }

        public IoCContainer WithActiveSite(SiteName siteName)
        {
            var b = builder.ConfigureServices(servise => servise
                        .AddSingleton<ISite>(x => GetSiteFromSwitcher(siteName)));

            var s = new List<SiteName>(sites);
            var activeS = new List<SiteName>(activeSites);

            if (!sites.Contains(siteName)) s.Add(siteName);
            if (!activeSites.Contains(siteName)) activeS.Add(siteName);

            return new IoCContainer(b, s, activeS, inactiveSites);
        }

        public IoCContainer WithInactiveSite(SiteName siteName)
        {
            var mock = new Mock<IGetRequest>();
            mock.Setup(x => x.Run()).Callback(() => { });
            mock.Setup(x => x.Connect).Returns(false);

            var b = builder.ConfigureServices(servise => servise
                        .AddSingleton<ISite>(x => GetSiteFromSwitcher(siteName).WithGetRequest(mock.Object)));

            var s = new List<SiteName>(sites);
            var inactiveS = new List<SiteName>(inactiveSites);

            if (!sites.Contains(siteName)) s.Add(siteName);
            if (!inactiveSites.Contains(siteName)) inactiveS.Add(siteName);

            return new IoCContainer(b, s, activeSites, inactiveS);
        }

        public IoCContainer WithActiveSites(List<SiteName> siteNames)
        {
            var container = new IoCContainer(this);
            foreach(var site in siteNames)
            {
                container = container.WithActiveSite(site);
            }

            return container;
        }

        public IoCContainer WithInactiveSites(List<SiteName> siteNames)
        {
            var container = new IoCContainer(this);
            foreach (var site in siteNames)
            {
                container = container.WithInactiveSite(site);
            }

            return container;
        }

        public IoCContainer WithRemoveSite(SiteName siteName)
        {
            var container = new IoCContainer()
                .WithActiveSites(activeSites.Where(x => x != siteName).ToList())
                .WithInactiveSites(inactiveSites.Where(x => x != siteName).ToList());

            return container;
        }

        public IoCContainer WithTurnOnSite(SiteName siteName)
        {
            if (!inactiveSites.Contains(siteName))
            {
                return this;
            }

            var container = new IoCContainer(this)
                .WithRemoveSite(siteName)
                .WithActiveSite(siteName);

            return container;
        }

        public IoCContainer WithTurnOffSite(SiteName siteName)
        {
            if (!activeSites.Contains(siteName))
            {
                return this;
            }

            var container = new IoCContainer(this)
                .WithRemoveSite(siteName)
                .WithInactiveSite(siteName);

            return container;
        }

        public List<ISite> GetSites()
        {
            return builder.Build().Services.GetServices<ISite>().ToList();
        }
    }
}
