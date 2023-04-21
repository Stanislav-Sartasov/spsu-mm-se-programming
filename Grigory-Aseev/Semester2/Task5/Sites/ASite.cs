using SiteInterfaces;
using WeatherTools;

namespace Sites
{
    public abstract class ASite : ISite
    {
        public Weather? WeatherInfo { get; private protected set; }
        public string SiteAddress { get; private set; }
        private string url { get; set; }
        private protected string? response { get; private set; }
        private protected bool requestSuccess { get; private set; }
        public string ExceptionMessages { get; private protected set; }

        public ASite(string name, string url)
        {
            SiteAddress = name;
            ExceptionMessages = "No errors detected.";
            this.url = url;
        }

        public bool GetRequest()
        {
            Exception? e;
            (response, e) = APIManagerTools.APIManager.GetResponse(url);

            if (e is not null)
            {
                ExceptionMessages = response + $"\nException from request message: {e.Message}";
            }

            return requestSuccess = e is null;
        }

        public void Clear()
        {
            WeatherInfo = null;
            response = null;
            requestSuccess = false;
            ExceptionMessages = "No errors detected.";
        }

        public abstract bool Parse();
    }
}
