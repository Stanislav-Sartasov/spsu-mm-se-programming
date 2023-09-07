using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThreadPoolTests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void ThreadPoolTest()
		{
			int testVar = 0;

			using (var threadPool = new ThreadPoolApp.ThreadPool())
			{
				for (var i = 0; i < 10; i++)
				{
					threadPool.Enqueue(() => { testVar += 1; Thread.Sleep(10); });
				} 
				
				Thread.Sleep(1000);
			}

			Assert.AreEqual(10, testVar);
		}

		[TestMethod]
		public void ExceptionTest()
		{
			var threadPool = new ThreadPoolApp.ThreadPool();
			threadPool.Dispose();

			Assert.ThrowsException<ObjectDisposedException>(() => threadPool.Enqueue(() => { }));
		}
	}
}
