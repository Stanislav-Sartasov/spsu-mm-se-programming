namespace Task_5
{
	public interface IRequest
	{
		public string Response { get; }
		public bool Connected { get; }

		public void Run(string address);
	}
}