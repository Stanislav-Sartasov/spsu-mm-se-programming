using System;
using System.Threading;
using NUnit.Framework;

namespace ThreadPool.UnitTests;

public class ThreadPoolTests
{
	[Test]
	public void ThreadPoolTest()
	{
		int testVar = 0;

		using (var threadPool = new ThreadPool())
		{
			threadPool.Enqueue(() => testVar += 10);
			Thread.Sleep(1);
		}

		Assert.AreEqual(10, testVar);
	}

	[Test]
	public void DisposedExceptionTest()
	{
		var threadPool = new ThreadPool();
		threadPool.Dispose();

		var doNothing = () => { };
		Assert.Throws<ObjectDisposedException>(() => threadPool.Enqueue(doNothing));
	}
}