using System.ComponentModel;

namespace WeatherUI_Winforms.Model;

public class WeatherInfo : INotifyPropertyChanged
{
    private string tempC;
    private string tempF;
    private string humidity;
    private string cloudsPercent;
    private string precipitation;
    private string windSpeed;
    private string windDirection;
    public string TempC
    {
        get => tempC;
        set
        {
            tempC = value;
            OnPropertyChanged("TempC");
        }
    }
    public string TempF
    {
        get => tempF;
        set
        {
            tempF = value;
            OnPropertyChanged("TempF");
        }
    }
    public string Humidity
    {
        get => humidity;
        set
        {
            humidity = value;
            OnPropertyChanged("Humidity");
        }
    }
    public string CloudsPercent
    {
        get => cloudsPercent;
        set
        {
            cloudsPercent = value;
            OnPropertyChanged("CloudsPercent");
        }
    }
    public string Precipitation
    {
        get => precipitation;
        set
        {
            precipitation = value;
            OnPropertyChanged("Precipitation");
        }
    }
    public string WindSpeed
    {
        get => windSpeed;
        set
        {
            windSpeed = value;
            OnPropertyChanged("WindSpeed");
        }
    }
    public string WindDirection
    {
        get => windDirection;
        set
        {
            windDirection = value;
            OnPropertyChanged("WindDirection");
        }
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string PropertyName)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    #endregion
}