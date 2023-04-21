﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherWebAPI;
using DateTimeManager;

namespace WpfUI
{
    public partial class MainWindow : Window
    {
        private TomorrowParser tomorrowParser = new TomorrowParser();
        private StormGlassParser stormGlassParser = new StormGlassParser();

        public MainWindow()
        {
            InitializeComponent();

            weatherTable.Items.Add(tomorrowParser.GetWeather());
            weatherTable.Items.Add(stormGlassParser.GetWeather());
        }

        private void UpdateWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            Date.UpdateTime();

            weatherTable.Items.Clear();
            weatherTable.Items.Add(tomorrowParser.GetWeather());
            weatherTable.Items.Add(stormGlassParser.GetWeather());
        }
    }
}
