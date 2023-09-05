using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace ProducerConsumer.UnitTests;

public class ProducerTests
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
	public void ProduceTest()
	{
		var producer = new Producer(_sem, _list, "Producer", _tokenSrc);
		producer.Start();
		Thread.Sleep(1);
		producer.Stop();

		Assert.AreEqual(1, _list.Count);
	}
}