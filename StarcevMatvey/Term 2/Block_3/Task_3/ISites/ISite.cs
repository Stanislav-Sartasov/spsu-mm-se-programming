using Requests;

namespace ISites
{
    public interface ISite
    {
        public IGetRequest Request { get; }

        public void ShowWeather();
        public ISite WithGetRequest(IGetRequest getRequest);
    }
}