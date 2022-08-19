using StructureMap;
using WeatherManagerAPI;

namespace IoCContainer
{
    public class WeatherIoCContainer
    {
        public IEnumerable<AManagerAPI> GetTypesContainer(List<bool> sites)
        {
            var container = new Container(site =>
            {
                if (sites[0])
                {
                    site.For<AManagerAPI>().Use<TomorrowIO>();
                }
                if (sites[1])
                {
                    site.For<AManagerAPI>().Use<StormGlassIO>();
                }
            });

            return container.GetAllInstances<AManagerAPI>();
        }
    }
}