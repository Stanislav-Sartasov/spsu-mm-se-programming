using ExamSystem.Core.ExamSystems;

namespace ExamSystem.UnitTests;

public class ExamSystemTestCreator
{
	public static void TestAddUser(IExamSystem system, int userCount, int threadCount)
	{
		Assert.DoesNotThrow(() => AddUsers(system, userCount, threadCount));

		Assert.That(system.Count, Is.EqualTo(userCount * threadCount));

		Assert.DoesNotThrow(() => CheckContainsUsers(system, userCount, threadCount));

		Assert.DoesNotThrow(() => CheckContainsRemoveUsers(system, userCount, threadCount));

		Assert.That(system.Count, Is.EqualTo(0));
	}


	private static void AddUsers(IExamSystem system, int userCount, int threadCount)
	{
		const int threadOffset = 1000000;
		var threads = new List<Thread>();

		for (var i = 0; i < threadCount; i++)
		{
			var localVar = i;
			var thread = new Thread(
				() => AddThreadJob(system, userCount, threadOffset * localVar)
			);
			threads.Add(thread);
		}

		StartAndWaitThreads(threads);
	}

	private static void CheckContainsRemoveUsers(IExamSystem system, int userCount, int threadCount)
	{
		const int threadOffset = 1000000;
		var threads = new List<Thread>();

		for (var i = 0; i < threadCount; i++)
		{
			var localVar = i;
			var thread = new Thread(
				() => CheckContainsRemoveJob(system, userCount, threadOffset * localVar)
			);
			threads.Add(thread);
		}

		StartAndWaitThreads(threads);
	}

	private static void CheckContainsUsers(IExamSystem system, int userCount, int threadCount)
	{
		const int threadOffset = 1000000;
		var threads = new List<Thread>();

		for (var i = 0; i < threadCount; i++)
		{
			var localVar = i;
			var thread = new Thread(
				() => CheckContainsJob(system, userCount, threadOffset * localVar)
			);
			threads.Add(thread);
		}

		StartAndWaitThreads(threads);
	}

	private static void StartAndWaitThreads(List<Thread> threads)
	{
		foreach (var thread in threads)
			thread.Start();

		foreach (var thread in threads)
			thread.Join();
	}

	private static void AddThreadJob(IExamSystem system, int userCount, int threadOffset)
	{
		for (var i = 0; i < userCount; i++)
		{
			var commonId = threadOffset + i;
			system.Add(commonId, commonId);
		}
	}

	private static void CheckContainsJob(IExamSystem system, int userCount, int threadOffset)
	{
		for (var i = 0; i < userCount; i++)
		{
			var commonId = threadOffset + i;

			if (!system.Contains(commonId, commonId))
				Assert.Fail();
		}
	}

	private static void CheckContainsRemoveJob(IExamSystem system, int userCount, int threadOffset)
	{
		for (var i = 0; i < userCount; i++)
		{
			var commonId = threadOffset + i;
			if (!system.Contains(commonId, commonId))
				Assert.Fail();

			system.Remove(commonId, commonId);
		}
	}
}