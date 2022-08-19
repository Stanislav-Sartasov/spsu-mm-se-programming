using UIWeatherForecast;
using WeatherManagerAPI;

namespace WinFormsWeatherForecast
{
    public partial class FormStormGlassIO : Form
    {
        public WeatherForecast WeatherData { get; private set; }
        public List<string> WeatherDataWithUnits { get; private set; }
        public FormStormGlassIO()
        {
            InitializeComponent();
            WeatherData = new WeatherForecast();
            WeatherDataWithUnits = new List<string>();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                base.WndProc(ref m);
                if ((int)m.Result == 0x1)
                    m.Result = (IntPtr)0x2;
                return;
            }

            base.WndProc(ref m);
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ifrm = Application.OpenForms[0];
            ifrm.StartPosition = FormStartPosition.Manual;
            ifrm.Left = this.Left;
            ifrm.Top = this.Top;
            ifrm.Show();
        }

        private void GetWeather_Click(object sender, EventArgs e)
        {
            BindingSource bindingSource = new BindingSource();

            WeatherData.UpdateWeather(typeof(StormGlassIO));

            if (WeatherData.ForecastData != null)
            {
                SetWeatherDataWithUnits(WeatherData);
            }

            if (WeatherDataWithUnits.Count > 0)
            {
                SetWeatherDataOnOutput();
                WeatherDataWithUnits.Clear();
            }
            else
            {
                bindingSource.DataSource = WeatherData;
                ExceptionMessage.DataBindings.Clear();
                ExceptionMessage.DataBindings.Add(new Binding("Text", bindingSource, "ExceptionMessage"));
            }
        }

        private void SetWeatherDataWithUnits(WeatherForecast weatherData)
        {
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.TemperatureCelsius} °С");
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.TemperatureFahrenheit} °F");
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.CloudCover} %");
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.Humidity} %");
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.Precipitation} mm/hr");
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.WindDirection} deg");
            WeatherDataWithUnits.Add($"{weatherData.ForecastData.WindSpeed} m/s");
        }

        private void SetWeatherDataOnOutput()
        {
            TemperatureCelsius.Text = WeatherDataWithUnits[0];
            TemperatureFahrenheit.Text = WeatherDataWithUnits[1];
            CloudCover.Text = WeatherDataWithUnits[2];
            Humidity.Text = WeatherDataWithUnits[3];
            Precipitaion.Text = WeatherDataWithUnits[4];
            WindDirection.Text = WeatherDataWithUnits[5];
            WindSpeed.Text = WeatherDataWithUnits[6];
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
