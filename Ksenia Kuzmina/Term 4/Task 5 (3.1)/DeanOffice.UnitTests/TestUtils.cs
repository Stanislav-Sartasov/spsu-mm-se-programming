using DeanOffice.ExamSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOffice.UnitTests
{
	internal static class TestUtils
	{
		public static void ThreadTest(IExamSystem examSystem, int threadCount, int amount)
		{
			var threads = new List<Thread>();

			for (int i = 0; i < threadCount; i++)
			{
				int offset = i * 100000;
				threads.Add(new Thread(() => AddTest(examSystem, amount, offset)));
				threads[i].Start();
			}

			for (int i = 0; i < threadCount; i++)
			{
				threads[i].Join();
			}

			threads.Clear();
			Assert.AreEqual(amount * threadCount, examSystem.Count);

			for (int i = 0; i < threadCount; i++)
			{
				int offset = i * 100000;
				threads.Add(new Thread(() => ContainsTest(examSystem, amount, offset)));
				threads[i].Start();
			}

			for (int i = 0; i < threadCount; i++)
			{
				threads[i].Join();
			}

			threads.Clear();
			Assert.AreEqual(amount * threadCount, examSystem.Count);

			for (int i = 0; i < threadCount; i++)
			{
				int offset = i * 100000;
				threads.Add(new Thread(() => RemoveTest(examSystem, amount, offset)));
				threads[i].Start();
			}

			for (int i = 0; i < threadCount; i++)
			{
				threads[i].Join();
			}

			threads.Clear();
			Assert.AreEqual(0, examSystem.Count);
			
		}

		private static void AddTest(IExamSystem examSystem, int amount, int offset)
		{
			for (var i = 0; i < amount; i++)
			{
				examSystem.Add(i + offset, i + offset + 100);
			}
		}

		private static void ContainsTest(IExamSystem examSystem, int amount, int offset)
		{
			for (var i = 0; i < amount; i++)
			{
				Assert.IsTrue(examSystem.Contains(i + offset, i + offset + 100));
			}
		}

		private static void RemoveTest(IExamSystem examSystem, int amount, int offset)
		{
			for (var i = 0; i < amount; i++)
			{
				examSystem.Remove(i + offset, i + offset + 100);
			}
		}
	}
}
