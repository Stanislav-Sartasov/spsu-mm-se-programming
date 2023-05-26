using DeanOffice.ExamSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOffice.UnitTests
{
	internal class StripedCuckooExamSystemTests
	{
		[Test]
		public void TestOneThread()
		{
			var examSystem = new StripedCuckooExamSystem();

			TestUtils.ThreadTest(examSystem, 1, 10000);
		}

		[Test]
		public void TestFourThreads()
		{
			var examSystem = new StripedCuckooExamSystem();

			TestUtils.ThreadTest(examSystem, 4, 100);
		}

		[Test]
		public void TestTenThousandFourThreads()
		{
			var examSystem = new StripedCuckooExamSystem();

			TestUtils.ThreadTest(examSystem, 4, 10000);
		}

		[Test]
		public void TestThousandSixteenThreads()
		{
			var examSystem = new StripedCuckooExamSystem();

			TestUtils.ThreadTest(examSystem, 16, 1000);
		}
	}
}
