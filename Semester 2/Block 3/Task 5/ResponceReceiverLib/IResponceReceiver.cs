namespace ResponceReceiverLib
{
	public interface IResponceReceiver
	{
		bool IsSucceed { get; }
		string Responce { get; }
	}
}
