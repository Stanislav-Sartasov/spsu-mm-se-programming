using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;
using WeatherUI_WPF.Model;
using WeatherUI_WPF.Service;
using WeatherUI_WPF.Service.Commands;

namespace WeatherUI_WPF.ViewModel
{

    public class WeatherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IApi api;

        private bool isOpenWeatherMap;
        private bool isTomorrowIO;
        private RelayCommand updateCommand;

        public string TempC { get; set; }
        public string TempF { get; set; }
        public string Humidity { get; set; }
        public string CloudsPercent { get; set; }
        public string Precipitation { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }

        public void UpdateWeather()
        {
            GetWeather();
            OnPropertyChanged("TempC");
            OnPropertyChanged("TempF");
            OnPropertyChanged("Humidity");
            OnPropertyChanged("CloudsPercent");
            OnPropertyChanged("Precipitation");
            OnPropertyChanged("WindSpeed");
            OnPropertyChanged("WindDirection");
            OnPropertyChanged("IsOpenWeatherMap");
            OnPropertyChanged("IsTomorrowIO");
            OnPropertyChanged("PropertyChanged");
        }

        public void GetWeather()
        {
            try
            {
                TempC = "";
                TempF = "";
                Humidity = "";
                CloudsPercent = "";
                Precipitation =  "";
                WindSpeed = "";
                WindDirection = "";

                if (IsOpenWeatherMap)
                {
                    api = IoCContainer.Container().First(x => x.ApiName == "OpenWeatherMap");
                    var data = api.GetData();
                    TempC += data.TempC;
                    TempF += data.TempF;
                    Humidity += data.Humidity;
                    CloudsPercent += data.CloudsPercent;
                    Precipitation += data.Precipitation;
                    WindSpeed += data.WindSpeed;
                    WindDirection += data.WindDirection;
                }

                if (IsOpenWeatherMap && IsTomorrowIO)
                {
                    TempC += " | ";
                    TempF += " | ";
                    Humidity += " | ";
                    CloudsPercent += " | ";
                    Precipitation += " | ";
                    WindSpeed += " | ";
                    WindDirection += " | ";
                }
                if (IsTomorrowIO)
                {
                    api = IoCContainer.Container().First(x => x.ApiName == "TomorrowIO");
                    var data = api.GetData();
                    TempC += data.TempC;
                    TempF += data.TempF;
                    Humidity += data.Humidity;
                    CloudsPercent += data.CloudsPercent;
                    Precipitation += data.Precipitation;
                    WindSpeed += data.WindSpeed;
                    WindDirection += data.WindDirection;
                }
            }
            catch (Exception ex)
            {
                TempC = "ERROR";
                TempF = "ERROR";
                Humidity = "ERROR";
                CloudsPercent = "ERROR";
                Precipitation = "ERROR";
                WindSpeed = "ERROR";
                WindDirection = "ERROR";
                MessageBox.Show(ex.Message, "ERROR",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsOpenWeatherMap
        {
            get => isOpenWeatherMap;
            set
            {
                isOpenWeatherMap = value;
                UpdateWeather();
            }
        }
        public bool IsTomorrowIO
        {
            get => isTomorrowIO;
            set
            {
                isTomorrowIO = value;
                UpdateWeather();
            }
        }

        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                       (updateCommand = new RelayCommand(obj =>
                       {
                           UpdateWeather();
                       }));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public WeatherViewModel() { }
    }
}