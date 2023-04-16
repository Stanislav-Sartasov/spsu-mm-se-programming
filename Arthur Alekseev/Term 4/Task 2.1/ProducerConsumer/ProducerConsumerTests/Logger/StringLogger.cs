using ILogger = ProducerConsumer.Interfaces.ILogger;

namespace ProducerConsumerTests.Logger
{
	internal class StringLogger : ILogger
	{
		public List<string> FullLog;

		public StringLogger()
		{
			FullLog = new List<string>();
		}

		public void Log(string message)
		{
			FullLog.Add(message);
		}
	}
}
