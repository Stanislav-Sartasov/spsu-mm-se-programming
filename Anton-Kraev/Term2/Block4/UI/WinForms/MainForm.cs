using WeatherLib.Container;
using WeatherLib.ISite;
using WeatherLib.WeatherInfo;

namespace WinForms
{
    public partial class MainForm : Form
    {
        List<IWeatherService> services = IoCContainer.GetServices();

        public MainForm()
        {
            InitializeComponent();
            foreach(var service in services)
            {
                service.CurrentWeather = new Weather();
            }
        }

        private void UpdateButtonClick(object sender, EventArgs e)
        {
            UpdateButton.Enabled = false;
            UpdateButton.Text = "Loading...";
            
            foreach (var service in services)
                service.UpdateWeather();

            OwmCloudinessValue.Text = services[0].CurrentWeather.Cloudiness.ToString();
            OwmHumidityValue.Text = services[0].CurrentWeather.Humidity.ToString();
            OwmTempCValue.Text = services[0].CurrentWeather.TempInCelsius.ToString();
            OwmTempFValue.Text = services[0].CurrentWeather.TempInFahrenheit.ToString();
            OwmWindDirectionValue.Text = services[0].CurrentWeather.WindDirection.ToString();
            OwmWindSpeedValue.Text = services[0].CurrentWeather.WindSpeed.ToString();
            OwmPrecipitationValue.Text = services[0].CurrentWeather.Precipitation.ToString();

            TioCloudinessValue.Text = services[1].CurrentWeather.Cloudiness.ToString();
            TioHumidityValue.Text = services[1].CurrentWeather.Humidity.ToString();
            TioTempCValue.Text = services[1].CurrentWeather.TempInCelsius.ToString();
            TioTempFValue.Text = services[1].CurrentWeather.TempInFahrenheit.ToString();
            TioWindDirectionValue.Text = services[1].CurrentWeather.WindDirection.ToString();
            TioWindSpeedValue.Text = services[1].CurrentWeather.WindSpeed.ToString();
            TioPrecipitationValue.Text = services[1].CurrentWeather.Precipitation.ToString();

            Time.Text = "Weather current at " + DateTime.Now.ToString("HH:mm");
            UpdateButton.Enabled = true;
            UpdateButton.Text = "Update info";
        }
    }
}