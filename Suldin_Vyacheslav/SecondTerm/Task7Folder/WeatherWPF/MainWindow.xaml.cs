using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UILogicLibrary;

namespace WeatherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WeatherModel model = new WeatherModel();
        private List<Label> labels = new List<Label>();
        public MainWindow()
        {
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
            List<List<string>> information = model.GetWeather();

            int i = 0;
            foreach (var service in information)
            {
             
                foreach (var term in service)
                {
                    if (i % 8 == 0 && model.GetError(term) != "")
                    {
                        labels[i].Content = term;
                        i += 8;
                    }
                    else
                    {
                        labels[i].Content = term;
                        i++;
                    }
                
                }
                
            }
        }

        public void SetKeys(object sender, RoutedEventArgs e)
        {
            var services = model.services;
            foreach (var service in services)
            {
                UIElement keyBox = (UIElement)this.FindName(service.GetType().Name);
                if (keyBox is TextBox && ((TextBox)keyBox).Text != "")
                {
                    service.SetKey(((TextBox)keyBox).Text);
                    ((TextBox)keyBox).Text = "";
                }
            }
        }

    }
}
