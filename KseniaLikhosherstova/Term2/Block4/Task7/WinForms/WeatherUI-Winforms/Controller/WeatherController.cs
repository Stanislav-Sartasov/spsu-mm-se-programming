using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherUI_Winforms.Model;
using WeatherUI_Winforms.View;
using WeatherUI_WPF.Service;

namespace WeatherUI_Winforms.Controller
{
    public class WeatherController
    {
        IApi api;

        public WeatherView View;

        public WeatherInfo Model;

        public bool IsOpenWeatherMap { get; set; }
        public bool IsTomorrowIO { get; set; }

        public WeatherController (WeatherView view)
        {
            Model = new WeatherInfo();
            View = view;
            View.Controller = this;
        }

        public void UpdateWeather()
        {
            try
            {
                Model.TempC = "";
                Model.TempF = "";
                Model.Humidity = "";
                Model.CloudsPercent = "";
                Model.Precipitation = "";
                Model.WindSpeed = "";
                Model.WindDirection = "";

                if (IsOpenWeatherMap)
                {
                    api = IoCContainer.Container().First(x => x.ApiName == "OpenWeatherMap");
                    var data = api.GetData();
                    Model.TempC += data.TempC;
                    Model.TempF += data.TempF;
                    Model.Humidity += data.Humidity;
                    Model.CloudsPercent += data.CloudsPercent;
                    Model.Precipitation += data.Precipitation;
                    Model.WindSpeed += data.WindSpeed;
                    Model.WindDirection += data.WindDirection;
                }

                if (IsOpenWeatherMap && IsTomorrowIO)
                {
                    Model.TempC += " | ";
                    Model.TempF += " | ";
                    Model.Humidity += " | ";
                    Model.CloudsPercent += " | ";
                    Model.Precipitation += " | ";
                    Model.WindSpeed += " | ";
                    Model.WindDirection += " | ";
                }
                if (IsTomorrowIO)
                {
                    api = IoCContainer.Container().First(x => x.ApiName == "TomorrowIO");
                    var data = api.GetData();
                    Model.TempC += data.TempC;
                    Model.TempF += data.TempF;
                    Model.Humidity += data.Humidity;
                    Model.CloudsPercent += data.CloudsPercent;
                    Model.Precipitation += data.Precipitation;
                    Model.WindSpeed += data.WindSpeed;
                    Model.WindDirection += data.WindDirection;
                }
            }
            catch (Exception ex)
            {
                Model.TempC = "ERROR";
                Model.TempF = "ERROR";
                Model.Humidity = "ERROR";
                Model.CloudsPercent = "ERROR";
                Model.Precipitation = "ERROR";
                Model.WindSpeed = "ERROR";
                Model.WindDirection = "ERROR";
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
