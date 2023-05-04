using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace ProducerConsumer.UnitTests;

public class ConsumerTests
{
	private List<int> _list;
	private Semaphore _sem = new Semaphore(1, 1);
	private CancellationTokenSource _tokenSrc;

	[SetUp]
	public void SetUp()
	{
		_list = new List<int>();
		_tokenSrc = new CancellationTokenSource();
	}

	[Test]
	public void ConsumeTest()
	{
		_list = new List<int> {0, 1, 2};

		var consumer = new Consumer(_sem, _list, "Consumer", _tokenSrc);
		consumer.Start();
		Thread.Sleep(1);
		consumer.Stop();

		Assert.AreEqual(new List<int> {1, 2}, _list);
	}
}