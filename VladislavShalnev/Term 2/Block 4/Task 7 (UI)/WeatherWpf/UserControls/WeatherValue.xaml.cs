using System;
using System.Windows;
using System.Windows.Controls;

namespace WeatherWpf.UserControls;

public partial class WeatherValue : UserControl
{
	public static readonly DependencyProperty IconProperty;
	public static readonly DependencyProperty ValueProperty;

	static WeatherValue()
	{
		IconProperty = DependencyProperty.Register(
			"Icon",
			typeof(Uri),
			typeof(WeatherValue)
		);
		ValueProperty = DependencyProperty.Register(
			"Value",
			typeof(string),
			typeof(WeatherValue)
		);
	}
	
	public Uri Icon
	{
		get => (Uri)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}
	public string Value
	{
		get => (string)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public WeatherValue()
	{
		InitializeComponent();
	}
}