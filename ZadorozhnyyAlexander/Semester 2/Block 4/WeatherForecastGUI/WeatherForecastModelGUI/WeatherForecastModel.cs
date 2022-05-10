using AbstractWeatherForecast;
using DataParsers;
using IoC;
using OpenweatherWeatherForecast;
using StormglassWeatherForecast;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using Tulpep.NotificationWindow;


namespace WeatherForecastModelGUI
{
    public class WeatherForecastModel : INotifyPropertyChanged
    {
        private const string openweatherKey = "b13eb5267bf4c9746e2f70d69a172b94";
        private const string stormglassKey = "b720d1e4-c40a-11ec-844a-0242ac130002-b720d252-c40a-11ec-844a-0242ac130002";

        private AWeatherForecast nowWeatherForecastSite;

        private PopupNotifier popup = null;

        private ContainerConfig weatherContainer;
        private SiteTypes siteType = SiteTypes.Openweather;

        public string Data { get; set; }
        public string ErrorMessage { get; set; }
        public string Description { get; set; }

        public WeatherForecastModel()
        {
            weatherContainer = new ContainerConfig()
            .AddService<OpenweatherForecast, OpenweatherParser>(openweatherKey, (int)SiteTypes.Openweather)
            .AddService<StormglassForecast, StormglassParser>(stormglassKey, (int)SiteTypes.Stormglass);

            PopupInintiolize();

            UpdateData(MessageTypes.StartStatus);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void PopupInintiolize()
        {
            popup = new PopupNotifier();

            popup.Image = Properties.Resources.popup;
            popup.ImageSize = new Size(96, 96);

            popup.TitleColor = Color.Azure;
            popup.HeaderColor = Color.Red;

            popup.ContentFont = new Font(FontFamily.GenericSerif, 14);

            popup.ShowCloseButton = false;

            popup.TitleText = "Notification";
        }

        public void SwitchService()
        {
            siteType = siteType == SiteTypes.Openweather ? SiteTypes.Stormglass : SiteTypes.Openweather;
            UpdateData(MessageTypes.SwitchStatus);
        }

        public void UpdateData(MessageTypes message = MessageTypes.UpdateStatus)
        {
            switch (siteType)
            {
                case SiteTypes.Stormglass:
                    nowWeatherForecastSite = weatherContainer.GetWeatherForecast<StormglassForecast>();
                    break;
                case SiteTypes.Openweather:
                    nowWeatherForecastSite = weatherContainer.GetWeatherForecast<OpenweatherForecast>();
                    break;
            }

            try
            {
                nowWeatherForecastSite.Update();

                switch (message)
                {
                    case MessageTypes.UpdateStatus:
                        popup.ContentText = "Data was succesfully updated!";
                        popup.Popup();
                        break;
                    case MessageTypes.SwitchStatus:
                        popup.ContentText = "You were swithed to another site!";
                        popup.Popup();
                        break;
                }
            }
            catch (NullReferenceException)
            {
                popup.ContentText = "Service unavailable!\n" +
                    "Try to update/turn on or wait";
                popup.Popup();
            }
            
            catch (Exception ex)
            {
                popup.ContentText = $"Service unavailable - {ex.Message}";
                popup.Popup();
            }

            CreateDataString();
            CreateDescription();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
        }

        public void UpdateServiceActivityStatus()
        {
            switch (siteType)
            {
                case SiteTypes.Openweather:
                    if (weatherContainer.IsServiceActive<OpenweatherForecast>())
                    {
                        weatherContainer.RemoveService<OpenweatherForecast>();
                        popup.ContentText = "Service Openweather was turned off.";
                        popup.Popup();
                    }
                    else
                    {
                        weatherContainer.AddService<OpenweatherForecast, OpenweatherParser>(openweatherKey, (int)SiteTypes.Openweather);
                        popup.ContentText = "Service Openweather was turned on.";
                        popup.Popup();
                    }
                    break;
                case SiteTypes.Stormglass:
                    if (weatherContainer.IsServiceActive<StormglassForecast>())
                    {
                        weatherContainer.RemoveService<StormglassForecast>();
                        popup.ContentText = "Service Stormglass was turned off.";
                        popup.Popup();
                    }
                    else
                    {
                        weatherContainer.AddService<StormglassForecast, StormglassParser>(stormglassKey, (int)SiteTypes.Stormglass);
                        popup.ContentText = "Service Stormglass was turned on.";
                        popup.Popup();
                    }
                    break;
            }
        }

        private void CreateDataString()
        {
            Data = $"Celsius Temperature : Nothing.\n" +
                $"Fahrenheit Temperature : Nothing.\n" +
                $"Cloud Cover : Nothing.\n" +
                $"Precipitation : Nothing.\n" +
                $"Air Humidity : Nothing.\n" +
                $"Wild Direction : Nothing.\n" +
                $"Wild Speed : Nothing.";
            Regex reg = new Regex("Nothing");

            if (nowWeatherForecastSite == null)
                return;

            foreach (var item in nowWeatherForecastSite.ShowFullWeatherForecast())
            {
                Data = reg.Replace(Data, item, 1);
            }
        }

        private void CreateDescription()
        {
            Description = "Weather Forecast from site ";
            if (siteType == SiteTypes.Stormglass)
                Description += "Stormglass";
            else
                Description += "Openweather";
        }
    }
}