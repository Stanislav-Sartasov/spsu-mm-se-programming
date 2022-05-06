using WeatherTools;

namespace SiteInterfaces
{
    public interface ISite
    {
        Weather? WeatherInfo { get; }
        string SiteAddress { get; }
        string ExceptionMessages { get; }
        bool GetRequest();
        bool Parse();
        void Clear();
    }
}
