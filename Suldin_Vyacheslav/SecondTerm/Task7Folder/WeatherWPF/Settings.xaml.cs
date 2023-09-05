using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Parsers;

namespace WeatherWPF
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public List<JSONParser> services;
        public Settings(List<JSONParser> services)
        {
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
                    service.SetKey(keyBox.Text);
                    conditionLabel.Content = "Success!";
                    return;
                }
            }
            conditionLabel.Content = "Wrong service";
        }
    }
}
