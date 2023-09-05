using System;
using System.ComponentModel;
using System.Windows;

namespace ChatApplication
{
    public partial class ChatWindow : Window
    {
        private readonly MainWindow mainWindow;

        public ChatWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = mainWindow.SendMessage(MessageBox.Text);
            if (success)
            {
                MessageBox.Text = "";
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            mainWindow.Dispose();
        }

        public void PrintMessage(string message)
        {
            ChatBox.Dispatcher.Invoke(new Action(() => ChatBox.Text += message));
        }
    }
}
