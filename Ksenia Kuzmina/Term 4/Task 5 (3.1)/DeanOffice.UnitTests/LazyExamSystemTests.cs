using System.Net.WebSockets;
using DeanOffice.ExamSystems;
using DeanOffice.UnitTests;

namespace ExamSystem.UnitTests
{
	public class LazyExamSystemTests
	{
		[Test]
		public void TestOneThread()
		{
			var examSystem = new LazyExamSystem();

			TestUtils.ThreadTest(examSystem, 1, 10000);
		}

		[Test]
		public void TestFourThreads()
		{
			var examSystem = new LazyExamSystem();

			TestUtils.ThreadTest(examSystem, 4, 100);
		}

		[Test]
		public void TestTenThousandFourThreads()
		{
			var examSystem = new LazyExamSystem();

			TestUtils.ThreadTest(examSystem, 4, 10000);
		}

		[Test]
		public void TestThousandSixteenThreads()
		{
			var examSystem = new LazyExamSystem();

			TestUtils.ThreadTest(examSystem, 16, 1000);
		}
	}
}