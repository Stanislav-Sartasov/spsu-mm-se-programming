using System;
using System.Collections.ObjectModel;
using System.Net;
using P2PChat.UI.WPF.Core;
using P2PChat.UI.WPF.MVVM.Model;

namespace P2PChat.UI.WPF.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
	private string? _currentEndpoint;

	private string _message;

	private string _name;


	private ChatModel? _selectedChat;

	public MainViewModel()
	{
		_currentEndpoint = "";
		_message = "";
		_name = new Random().Next().ToString();
		Chats = new ObservableCollection<ChatModel>();
		Messages = new ObservableCollection<MessageModel>();

		SendCommand = new RelayCommand(_ =>
		{
			_selectedChat?.SendMessage(Message);
			Message = "";
		});

		AddChat = new RelayCommand(_ => { Chats.Add(new ChatModel(_name)); });

		JoinEndpoint = new RelayCommand(_ =>
		{
			if (_currentEndpoint is null || _selectedChat is null)
				return;

			var result = IPEndPoint.TryParse(_currentEndpoint, out var endpoint);
			if (result && endpoint is not null)
				_selectedChat.Join(endpoint);
		});

		ChangeName = new RelayCommand(_ =>
		{
			foreach (var chat in Chats) chat.ChangeName(_name);
		});

		DisposeChats = new RelayCommand(_ =>
		{
			foreach (var chat in Chats)
				chat.Dispose();
		});
	}

	public ObservableCollection<MessageModel> Messages { get; set; }
	public ObservableCollection<ChatModel> Chats { get; set; }

	public RelayCommand SendCommand { get; set; }
	public RelayCommand AddChat { get; set; }
	public RelayCommand JoinEndpoint { get; set; }
	public RelayCommand ChangeName { get; set; }
	public RelayCommand DisposeChats { get; set; }

	public ChatModel? SelectedChat
	{
		get => _selectedChat;
		set
		{
			_selectedChat = value;
			CurrentEndpoint = _selectedChat?.ConnectedEp;
			OnPropertyChanged();
		}
	}

	public string Message
	{
		get => _message;
		set
		{
			_message = value;
			OnPropertyChanged();
		}
	}

	public string? CurrentEndpoint
	{
		get => _currentEndpoint;
		set
		{
			_currentEndpoint = value;
			OnPropertyChanged();
		}
	}

	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			OnPropertyChanged();
		}
	}
}