using System;
using P2PChat.Core.Message.UserMessage;

namespace P2PChat.UI.WPF.MVVM.Model;

public class MessageModel
{
	public MessageModel(UserMessage msg, bool first, bool isMy)
	{
		Username = msg.Name;
		Message = msg.Content;
		FirstMessage = first;
		IsMy = isMy;
		UsernameColor = "#aabbcc";
		Time = DateTime.Now;
	}

	public string Username { get; set; }
	public string UsernameColor { get; set; }
	public string Message { get; set; }
	public DateTime Time { get; set; }
	public bool IsMy { get; set; }
	public bool? FirstMessage { get; set; }
}