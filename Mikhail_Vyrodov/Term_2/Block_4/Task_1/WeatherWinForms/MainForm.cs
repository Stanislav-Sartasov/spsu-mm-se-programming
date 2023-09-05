using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherClasses;
using System.Collections.ObjectModel;

namespace WeatherWinForms
{
    public partial class MainForm : Form
    {
        private StormglassParametersProvider stormglassParamsProvider;
        private TomorrowParametersProvider tomorrowParamsProvider;
        private WeatherCharacterization tomorrowCharacterization, stormglassCharacterization;

        public MainForm()
        {
            ResponseReader respReader = new ResponseReader();
            StormglassioWebHelper stormglassWebHelper = new StormglassioWebHelper();
            TomorrowioWebHelper tomorrowWebHelper = new TomorrowioWebHelper();
            stormglassParamsProvider = new StormglassParametersProvider(respReader, stormglassWebHelper);
            tomorrowParamsProvider = new TomorrowParametersProvider(respReader, tomorrowWebHelper);
            tomorrowCharacterization = new WeatherCharacterization();
            stormglassCharacterization = new WeatherCharacterization();

            tomorrowParamsProvider.FillWeatherProperties();
            tomorrowCharacterization = tomorrowParamsProvider.Weather;
            stormglassParamsProvider.FillWeatherProperties();
            stormglassCharacterization = stormglassParamsProvider.Weather;

            InitializeComponent();

            weatherBinding.Add(tomorrowCharacterization);
            weatherBinding.Add(stormglassCharacterization);

            tomorrowTemperatureTextBox.DataBindings.Add(new Binding("Text", weatherBinding[0], "Temperature", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            tomorrowWindSpeedTextBox.DataBindings.Add(new Binding("Text", weatherBinding[0], "WindSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            tomorrowWindDirectionTextBox.DataBindings.Add(new Binding("Text", weatherBinding[0], "WindDirection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            tomorrowHumidityTextBox.DataBindings.Add(new Binding("Text", weatherBinding[0], "Humidity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            tomorrowPrecipitationTextBox.DataBindings.Add(new Binding("Text", weatherBinding[0], "Precipitation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            tomorrowCloudCoverTextBox.DataBindings.Add(new Binding("Text", weatherBinding[0], "CloudCover", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

            stormglassTemperatureTextBox.DataBindings.Add(new Binding("Text", weatherBinding[1], "Temperature", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            stormglassWindSpeedTextBox.DataBindings.Add(new Binding("Text", weatherBinding[1], "WindSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            stormglassWindDirectionTextBox.DataBindings.Add(new Binding("Text", weatherBinding[1], "WindDirection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            stormglassHumidityTextBox.DataBindings.Add(new Binding("Text", weatherBinding[1], "Humidity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            stormglassPrecipitationTextBox.DataBindings.Add(new Binding("Text", weatherBinding[1], "Precipitation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            stormglassCloudCoverTextBox.DataBindings.Add(new Binding("Text", weatherBinding[1], "CloudCover", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            tomorrowParamsProvider.FillWeatherProperties();
            stormglassParamsProvider.FillWeatherProperties();
            tomorrowCharacterization.ChangeWeather(tomorrowParamsProvider.Weather);
            stormglassCharacterization.ChangeWeather(stormglassParamsProvider.Weather);
        }

    }
}
