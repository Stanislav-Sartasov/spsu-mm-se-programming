using System.Windows;
using System.Windows.Controls;

namespace WeatherWpf.UserControls;

public partial class WeatherCard : UserControl
{
	public static readonly DependencyProperty SourceProperty;

	static WeatherCard()
	{
		SourceProperty = DependencyProperty.Register(
			"Source",
			typeof(string),
			typeof(WeatherCard)
		);
	}
	
	public string Source
	{
		get => (string)GetValue(SourceProperty);
		set => SetValue(SourceProperty, value);
	}
	
	public WeatherCard()
	{
		InitializeComponent();
	}
}