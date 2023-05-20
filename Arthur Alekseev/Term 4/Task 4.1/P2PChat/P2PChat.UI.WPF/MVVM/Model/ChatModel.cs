using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Data;
using P2PChat.Core.Chat;
using P2PChat.Core.Log;
using P2PChat.Core.Message.UserMessage;

namespace P2PChat.UI.WPF.MVVM.Model;

public class ChatModel : IDisposable
{
	private readonly IChat _chat;
	private string _name;

	public ChatModel(string name)
	{
		_chat = new Chat(new ConsoleLogger());
		_chat.OnMessage += Receive;
		_chat.Start();
		_chat.ChangeName(name);
		_name = name;
		ChatName = _chat.Port.ToString();
		Messages = new ObservableCollection<MessageModel>
		{
			new(
				new UserMessage(
					$"Welcome to the chat!\n To join another chat use the text box above\n People can join you via ip:{_chat.Port}",
					"P2P Chat"),
				true, false
			)
		};
		BindingOperations.EnableCollectionSynchronization(Messages, Messages);

		Port = _chat.Port;
		CanJoin = true;
		ConnectedEp = "";
	}

	public string? ConnectedEp { get; set; }
	public string ChatName { get; set; }
	public int Port { get; set; }
	public bool CanJoin { get; private set; }
	public ObservableCollection<MessageModel> Messages { get; init; }

	public void Dispose()
	{
		_chat.Dispose();
	}

	public void SendMessage(string message)
	{
		_chat.Send(message);
		Messages.Add(new MessageModel(new UserMessage(message, _name), true, true));
	}

	private void Receive(UserMessage msg)
	{
		Messages.Add(new MessageModel(msg, true, false));
	}

	public void Join(IPEndPoint endpoint)
	{
		var joined = _chat.Join(endpoint);

		if (joined)
		{
			Messages.Add(new MessageModel(
				new UserMessage($"Joined chat {endpoint}", "P2P Chat"), true, false)
			);
			ConnectedEp = endpoint.ToString();
			CanJoin = false;
		}
		else
		{
			Messages.Add(new MessageModel(
				new UserMessage($"Cannot join {endpoint}, check the IP and Port", "P2P Chat"), true, false)
			);
		}
	}

	public void ChangeName(string name)
	{
		_name = name;
		_chat.ChangeName(name);
	}
}