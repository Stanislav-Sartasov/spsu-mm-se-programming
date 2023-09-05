using System.Windows.Input;
using WeatherForecastModelGUI;

namespace WpfWeatherForecastModel
{
    public class WeatherForecastModel : WeatherForecastModelGUI.WeatherForecastModel
    {
        public WeatherForecastModel() : base()
        {
            UpdateCommand = new UpdateDelegateCommand(UpdateData, MessageTypes.UpdateStatus);

            SwitchCommand = new StandartDelegateCommand(SwitchService);

            ActivityCommand = new StandartDelegateCommand(UpdateServiceActivityStatus);
        }

        public ICommand UpdateCommand
        {
            get; private set;
        }

        public ICommand SwitchCommand
        {
            get; private set;
        }

        public ICommand ActivityCommand
        {
            get; private set;
        }
    }
}