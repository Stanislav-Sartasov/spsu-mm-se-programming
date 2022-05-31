namespace WpfWeather;
using System.Windows;
using General;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Model model;
    public MainWindow()
    {
        model = new Model();
        InitializeComponent();
    }

    private void updateButtonClick(object sender, RoutedEventArgs e)
    {
        if (comboBox.SelectedItem != null)
        {
            weatherTextBox.Text = model.GetData((string)comboBox.SelectedItem);
        }
    }
}
