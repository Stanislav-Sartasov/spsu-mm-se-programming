using System.Net;

namespace P2PChat.UI;

public partial class MainPage : Form
{
	private Chat? _chat;
	private bool _isConnected;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnSendButtonClick(object sender, EventArgs e)
	{
		string text = msgBox.Text;
		if (text == "") return;

		msgBox.Clear();

		if (_chat == null)
		{
			CreateChat(text);
			return;
		}

		if (!_isConnected)
		{
			try
			{
				_chat.Connect(IPEndPoint.Parse(text));
			}
			catch (Exception ex)
			{
				AddChatItem($"Ошибка: {ex.Message}");
			}

			return;
		}

		_chat.Send(text);
	}

	private void CreateChat(string port)
	{
		try
		{
			_chat = new Chat(int.Parse(port));
			_chat.OnEvent += OnChatEvent;
			AddChatItem($"Чат работает на порте {port}");
			AddChatItem("Введите IP адрес с портом для подключения или дождитесь подключения к вам:");
		}
		catch
		{
			AddChatItem("Неправильный порт или порт уже занят.");
		}
	}

	private void OnChatEvent(ChatEvent e, IPEndPoint sender, string? payload)
	{
		switch (e)
		{
			case ChatEvent.Message:
				AddChatItem($"{sender}: {payload}");
				break;
			case ChatEvent.Connect:
				AddChatItem($"Подключено к чату {sender}");
				_isConnected = true;
				break;
			case ChatEvent.Disconnect:
				AddChatItem($"{sender} отключился от чата");
				break;
			case ChatEvent.Error:
				AddChatItem($"Ошибка {sender}: {payload}");
				break;
		}
	}

	private void AddChatItem(string message) => chatBox.Items.Add(message);

	private void OnClosing(object sender, FormClosingEventArgs e) => _chat?.Dispose();
}