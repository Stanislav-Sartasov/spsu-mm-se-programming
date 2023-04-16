﻿using ProducerConsumer.Interfaces;

namespace ProducerConsumer.Logging;

public class ConsoleLogger : ILogger
{
	public void Log(string message)
	{
		Console.WriteLine(message);
	}
}