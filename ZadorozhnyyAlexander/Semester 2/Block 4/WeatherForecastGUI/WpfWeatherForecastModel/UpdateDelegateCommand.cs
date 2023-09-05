using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherForecastModelGUI;

namespace WpfWeatherForecastModel
{
    public class UpdateDelegateCommand : ICommand
    {
        private readonly Action<MessageTypes> action;
        private readonly MessageTypes messageType;

        public event EventHandler? CanExecuteChanged;

        public UpdateDelegateCommand(Action<MessageTypes> action, MessageTypes messageType)
        {
            this.action = action;
            this.messageType = messageType;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.action(messageType);
        }
    }
}
