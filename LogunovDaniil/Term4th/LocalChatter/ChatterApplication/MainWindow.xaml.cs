using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using LocalChatter;

namespace ChatterApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ChatClient? _chatClient;

		public MainWindow()
		{
			InitializeComponent();

			var currentMachine = LocalChatter.Utils.NetworkHelper.GetCurrentMachineName();
			if (currentMachine == null)
			{
				ErrorMessageBox.Text = "No Network found! Connect to the local network and restart the application";
				return;
			}

			IPBlockText.Text = currentMachine;

			_chatClient = new(currentMachine, ReceiveMessage);
			_chatClient.Start();
		}

		private void ReceiveMessage(ChatMessage message)
		{
			var readableString = "\n" + LocalChatter.WindowUtils.ChatMessageConverter.ConvertToReadableString(message);
			ChatBox.Dispatcher.Invoke(new Action(() =>
			{
				AddToChatBox(readableString);
			}));
		}

		private void AddToChatBox(string message)
		{
			ChatBox.AppendText(message);
			ChatBox.ScrollToEnd();
		}

		private void SendMessage(object sender, RoutedEventArgs e)
		{
			if (_chatClient == null) return;
			_chatClient.SendMessage(MessageBox.Text);
			MessageBox.Clear();
		}

		private void DisposeWindow(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_chatClient == null) return;
			_chatClient.Dispose();
		}

		private void DisconnectFromCurrent(object sender, RoutedEventArgs e)
		{
			if (_chatClient == null) return;
			_chatClient.Disconnect();
			AddToChatBox("\nDisconnected from current session!");
		}

		private void JoinSession(object sender, RoutedEventArgs e)
		{
			if (_chatClient == null) return;
			DisconnectFromCurrent(sender, e);
			var connectTo = JoinClientBox.Text;
			if (_chatClient.ConnectToClient(connectTo))
			{
				JoinClientBox.Clear();
				ErrorMessageBox.Text = String.Empty;
				return;
			}
			ErrorMessageBox.Text = "Could not join stated client! Check the name and try again";
		}
	}
}
