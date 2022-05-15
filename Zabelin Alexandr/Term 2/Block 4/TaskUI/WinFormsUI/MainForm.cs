using WeatherLibrary;
using WeatherWebAPI;
using System;
using DateTimeManager;

namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        private TomorrowParser tomorrowParser = new TomorrowParser();
        private StormGlassParser stormGlassParser = new StormGlassParser();
        private DateAndTime dateTime = new DateAndTime();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            bindingSourceDate.Add(dateTime);
            bindingSourceTomorrowIO.Add(tomorrowParser.GetWeather());
            bindingSourceStormGlass.Add(stormGlassParser.GetWeather());
        }

        private void updateWeatherButton_Click(object sender, EventArgs e)
        {
            bindingSourceTomorrowIO.RemoveAt(0);
            bindingSourceStormGlass.RemoveAt(0);

            dateTime.UpdateTime();
            bindingSourceTomorrowIO.Add(tomorrowParser.GetWeather());
            bindingSourceStormGlass.Add(stormGlassParser.GetWeather());
        }
    }
}