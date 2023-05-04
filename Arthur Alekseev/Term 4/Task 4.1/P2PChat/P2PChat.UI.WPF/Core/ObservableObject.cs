using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace P2PChat.UI.WPF.Core;

public class ObservableObject : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}