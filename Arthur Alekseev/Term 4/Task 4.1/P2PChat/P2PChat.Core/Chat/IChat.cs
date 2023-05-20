using System.Net;
using P2PChat.Core.Message.UserMessage;

namespace P2PChat.Core.Chat;

public delegate void MessageReceive(UserMessage message);

/// <summary>
///     Interface for peer to peer chat
/// </summary>
public interface IChat : IDisposable
{
	/// <summary>
	///     Get chat listener port
	/// </summary>
	public int Port { get; }

	/// <summary>
	///     Start chat
	/// </summary>
	public void Start();

	/// <summary>
	///     Send message
	/// </summary>
	/// <param name="message">message to be sent</param>
	/// <returns>true if operation was successful and false otherwise</returns>
	public bool Send(string message);

	/// <summary>
	///     Joins another chat via endpoint
	/// </summary>
	/// <param name="address">Endpoint of other chat</param>
	/// <returns>true if join was successful and false otherwise</returns>
	public bool Join(IPEndPoint address);

	/// <summary>
	///     Change name
	/// </summary>
	/// <param name="name">Your new name</param>
	public void ChangeName(string name);

	/// <summary>
	///     Stop the chat inner processes
	/// </summary>
	public void Stop();

	/// <summary>
	///     Get history of sent/received messages
	/// </summary>
	/// <returns></returns>
	public List<UserMessage> GetHistory();

	/// <summary>
	///     New Message Receive Event
	/// </summary>
	public event MessageReceive OnMessage;
}