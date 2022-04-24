using System.Collections.Generic;
using System.Windows;
using Parsers;
using IoC;
using GisMeteo;
using TomorrowIO;
using OpenWeather;
using StormGlass;

namespace WeatherWPF
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public List<JSONParser> services;

        public DependencyResolver dp;
        public Settings(List<JSONParser> services, DependencyResolver dp)
        {
            this.dp = dp;
            this.services = services;
            InitializeComponent();
            foreach (var service in services)
            {
                serviceBox.Items.Add(service.GetType().Name);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var service in services)
            {
                if (service.GetType().Name == serviceBox.Text)
                {
                    switch (serviceBox.Text)
                    {
                        case "GisMeteoParser":
                            {
                                services[services.IndexOf(service)] = dp.GetService<GisMeteoParser>(keyBox.Text);
                                break;
                            }
                        case "TomorrowIOParser":
                            {
                                services[services.IndexOf(service)] = dp.GetService<TomorrowIOParser>(keyBox.Text);
                                break;
                            }
                        case "OpenWeatherParser":
                            {
                                services[services.IndexOf(service)] = dp.GetService<OpenWeatherParser>(keyBox.Text);
                                break;
                            }
                        case "StormGlassParser":
                            {
                                services[services.IndexOf(service)] = dp.GetService<StormGlassParser>(keyBox.Text);
                                break;
                            }
                    }
                    conditionLabel.Content = "Success!";
                    return;
                }
            }
            conditionLabel.Content = "Wrong service(";
            return;
        }
    }
}
