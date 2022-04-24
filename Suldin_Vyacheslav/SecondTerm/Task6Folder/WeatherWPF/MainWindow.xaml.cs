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
using IoC;

namespace WeatherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Label> labels = new List<Label>();

        private DependencyResolver dp = new DependencyResolver();

        private Settings settings;

        private List<JSONParser> services;

        public readonly int FieldCount = 8;
        public MainWindow()
        {
            //dp.Remove<StormGlassParser>();    

            services = dp.GetParsers();
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

            

            int i = 0;
            foreach (var service in services)
            {

                var information = service.GetWeatherInfo();


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
            services = dp.GetParsers();
        }

        public void SetKeys(object sender, RoutedEventArgs e)
        {

            if (settings == null || !settings.IsActive)
            {
                settings = new Settings(services, dp);
                settings.Show();
            }
            else settings.Activate();
        }

    }
}
