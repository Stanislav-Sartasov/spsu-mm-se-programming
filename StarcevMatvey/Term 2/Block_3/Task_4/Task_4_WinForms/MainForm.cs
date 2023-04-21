using ISites;
using Sites;
using System.Collections.Generic;

namespace Task_4_WinForms
{
    public partial class MainForm : Form
    {
        readonly List<ISite> sites = new List<ISite> { new OpenWeatherMap(), new TomorrowIo() };
        readonly int countOfSites = 2;
        private int index = 0;

        public MainForm()
        {
            InitializeComponent();

            ISite site = sites[index % countOfSites];
            siteName.Text = site.Name;
            SetWeather(site.GetWeather());
        }

        private void refreshClick(object sender, EventArgs e)
        {
            ISite site = sites[index % countOfSites];
            Weather.Weather weather = site.GetWeather();
            SetWeather(weather);
            MessageBox.Show("Refreshed");
        }

        private void swClick(object sender, EventArgs e)
        {
            ISite site = sites[++index % countOfSites];
            Weather.Weather weather = site.GetWeather();
            SetWeather(weather);
            siteName.Text = site.Name;
            MessageBox.Show("Swithed");
        }

        private void SetWeather(Weather.Weather weather)
        {
            tempValue.Text = weather.TempC + " " + weather.TempF;
            cloudsValue.Text = weather.Clouds;
            humidityValue.Text = weather.Humidity;
            windSpdValue.Text = weather.WindSpeed;
            windDegValue.Text = weather.WindDegree;
            falloutValue.Text = weather.FallOut;
        }
    }
}