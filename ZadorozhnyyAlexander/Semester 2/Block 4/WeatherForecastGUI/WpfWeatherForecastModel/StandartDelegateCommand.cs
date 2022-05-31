using System.Windows.Input;

namespace WpfWeatherForecastModel
{
    public class StandartDelegateCommand : ICommand
    {
        private readonly Action action;

        public event EventHandler? CanExecuteChanged;

        public StandartDelegateCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.action();
        }
    }
}