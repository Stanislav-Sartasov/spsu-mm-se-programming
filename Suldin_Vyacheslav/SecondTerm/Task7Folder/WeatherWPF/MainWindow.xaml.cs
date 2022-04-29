using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Parsers;
using StormGlass;
using WebLibrary;
using OpenWeather;
using GisMeteo;
using TomorrowIO;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WeatherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Label> labels = new List<Label>();

        private ResourceManager rm = new ResourceManager("WeatherWPF.BaseKeysSet", Assembly.GetExecutingAssembly());

        private List<JSONParser> services = new List<JSONParser>();

        private Settings settings;

        public readonly int FieldCount = 8;
        public MainWindow()
        {

            services = new List<JSONParser>() { new GisMeteoParser(rm.GetString("GisMeteoAPI")),
            new OpenWeatherParser(rm.GetString("OpenWeatherAPI")),
            new TomorrowIOParser(rm.GetString("TomorrowAPI")),
            new StormGlassParser(rm.GetString("StormGlassAPI"))};

            InitializeComponent();
            foreach (UIElement item in root.Children)
            {
                if (item is Label && ((Label)item).Content == "")
                {
                    labels.Add((Label)item);
                }
            }
        }

        private void ShowWeather(object sender, RoutedEventArgs e)
        {

            foreach (var label in labels)
            {
                label.Content = "";
            }
            //List<List<string>> information = model.GetWeather();

            int i = 0;
            foreach (var service in services)
            {
                var subList = new List<string>();
                var gr = new GetRequest(service.Link, service.Headers);
                var jg = new JsonGetter(gr);
                var json = jg.GetJSON();
                var information = service.Parse(json);


                labels[i].Content = information.Name;

                if (information.Error != null)
                {
                    labels[i].Content += ": " + Regex.Replace(information.Error, @"[^\d]+", ""); ;
                    i += FieldCount;
                }
                else
                {
                    labels[++i].Content = information.MetricTemp;
                    labels[++i].Content = information.ImperialTemp;
                    labels[++i].Content = information.CloudCover;
                    labels[++i].Content = information.Humidity;
                    labels[++i].Content = information.Precipipations;
                    labels[++i].Content = information.WindSpeed;
                    labels[++i].Content = information.WindDegree;
                    i++;
                }
            }
        }

        public void SetKeys(object sender, RoutedEventArgs e)
        {

            if (settings == null || !settings.IsActive)
            {
                settings = new Settings(services);
                settings.Show();
            }
            else settings.Activate();
        }

    }
}
