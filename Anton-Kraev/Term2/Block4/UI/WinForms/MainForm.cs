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
            {
                service.UpdateWeather();
            }
            UpdateData(OpenWeatherMap, services[0]);
            UpdateData(TomorrowIo, services[1]);

            Time.Text = "Weather current at " + DateTime.Now.ToString("HH:mm");
            UpdateButton.Enabled = true;
            UpdateButton.Text = "Update info";
        }

        private void UpdateData(GroupBox container, IWeatherService weatherService)
        {
            container.Controls[0].Text = weatherService.CurrentWeather?.Precipitation?.ToString() ?? "No data";
            container.Controls[1].Text = weatherService.CurrentWeather?.WindDirection?.ToString() ?? "No data";
            container.Controls[2].Text = weatherService.CurrentWeather?.WindSpeed?.ToString() ?? "No data";
            container.Controls[3].Text = weatherService.CurrentWeather?.Cloudiness?.ToString() ?? "No data";
            container.Controls[4].Text = weatherService.CurrentWeather?.Humidity?.ToString() ?? "No data";
            container.Controls[5].Text = weatherService.CurrentWeather?.TempInFahrenheit?.ToString() ?? "No data";
            container.Controls[6].Text = weatherService.CurrentWeather?.TempInCelsius?.ToString() ?? "No data";
        }
    }
}