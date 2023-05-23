using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat_App.Helpers
{
    internal class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if(EqualityComparer<T>.Default.Equals(x:field, y:value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }*/
    }
}
