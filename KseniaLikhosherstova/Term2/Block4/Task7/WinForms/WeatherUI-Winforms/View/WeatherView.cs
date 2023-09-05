using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherUI_Winforms.Controller;

namespace WeatherUI_Winforms.View
{
    public partial class WeatherView : Form
    {
        private WeatherController _controller;

        public WeatherController Controller
        {
            get => _controller;
            set
            {
                _controller = value;

                this.TempC.         DataBindings.Add("Text", Controller.Model, "TempC");
                this.TempF.         DataBindings.Add("Text", Controller.Model, "TempF");
                this.Humidity.      DataBindings.Add("Text", Controller.Model, "Humidity");
                this.CloudsPercent. DataBindings.Add("Text", Controller.Model, "CloudsPercent");
                this.Precipitation. DataBindings.Add("Text", Controller.Model, "Precipitation");
                this.WindSpeed.     DataBindings.Add("Text", Controller.Model, "WindSpeed");
                this.WindDirection. DataBindings.Add("Text", Controller.Model, "WindDirection");
            }
        }
        public WeatherView()
        {
            InitializeComponent();
        }

        private void WeatherView_Load(object sender, EventArgs e)
        {

        }

        private void isOpenWeather_CheckedChanged(object sender, EventArgs e)
        {
            Controller.IsOpenWeatherMap = isOpenWeather.Checked;
            Controller.UpdateWeather();
        }

        private void isTomorrowIO_CheckedChanged(object sender, EventArgs e)
        {
            Controller.IsTomorrowIO = isTomorrowIO.Checked;
            Controller.UpdateWeather();
        }

        private void UpdateButton_Click(object sender, EventArgs e) => Controller.UpdateWeather();
    }
}
