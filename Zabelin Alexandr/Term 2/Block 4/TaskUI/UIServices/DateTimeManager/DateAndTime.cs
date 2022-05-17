using System;
using System.ComponentModel;

namespace DateTimeManager
{
    public class DateAndTime : INotifyPropertyChanged
    {
        public DateTime Date { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public DateAndTime()
        {
            UpdateTime();
        }

        public void UpdateTime()
        {
            this.Date = DateTime.Now;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
        }
    }
}