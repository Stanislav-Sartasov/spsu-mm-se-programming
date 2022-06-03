using System.ComponentModel;
using IoCContainerTools;
using WeatherTools;

namespace UITools
{
    public class SiteData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Weather? forecast;
        private string? exception;

        public Weather? Forecast
        {
            get => forecast;
            private set
            {
                forecast = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Forecast)));
            }
        }

        public string? Exception
        {
            get => exception;
            private set
            {
                exception = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Exception)));
            }
        }

        public SiteData(Type type)
        {
            UpdateData(type);
        }

        public void UpdateData(Type type)
        {
            Forecast = null;
            Exception = null;

            for (int i = IoCContainer.Sites.Count - 1; i >= 0; i--)
            {
                IoCContainer.DisconnectSite(IoCContainer.Sites[i]);
            }

            IoCContainer.ConnectSite(type);

            var sites = IoCContainer.GetSites();

            foreach (var site in sites)
            {
                site.GetRequest();
                site.Parse();
                if (site.WeatherInfo is not null)
                {
                    Forecast = site.WeatherInfo;
                }
                if (!site.ExceptionMessages.Equals("No errors detected."))
                {
                    Exception = site.ExceptionMessages;
                }
            }
        }
    }
}