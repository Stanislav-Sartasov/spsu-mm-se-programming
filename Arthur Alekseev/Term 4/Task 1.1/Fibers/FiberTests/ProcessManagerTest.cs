using Fibers.Fibers;
using Fibers.ProcessManager;
using Moq;

namespace FiberTests;

public class ProcessManagerTest
{
	private List<string>? _log;

	[SetUp]
	public void SetUp()
	{
		_log = new List<string>();
	}

	[Test]
	public void RunWithPriorityTest()
	{
		Assert.DoesNotThrow(RunWithPriority);
	}

	private void RunWithPriority()
	{
		_log?.Clear();

		var mockFiberWrappers = GenMockFiberWrappers();

		// Switch will result in error if it is working with actual fibers
		ProcessManager.RunFibers(mockFiberWrappers, AlgorithmType.WithPriority);

		// Just need to assert all processes were running, finished and switched to primary
		for (var i = 0; i < 7; i++)
		{
			Assert.IsTrue(_log != null && _log.Contains("Switched to " + i + " not finished"));
			Assert.IsTrue(_log != null && _log.Contains("Switched to " + i + " finished"));
		}

		Assert.IsTrue(_log != null && _log.Contains("Switched to primary"));
	}

	[Test]
	public void RunWithNoPriorityTest()
	{
		Assert.DoesNotThrow(RunWithNoPriority);
	}

	private void RunWithNoPriority()
	{
		_log?.Clear();

		var mockFiberWrappers = GenMockFiberWrappers();

		// Switch will result in error if it is working with actual fibers here too
		ProcessManager.RunFibers(mockFiberWrappers, AlgorithmType.NoPriority);

		// As non-priority strategy does not depend on random expected and actual should be equal
		const string expected = @"Switched to 1 not finished
Switched to 2 not finished
Switched to 3 not finished
Switched to 4 not finished
Switched to 5 not finished
Switched to 6 not finished
Switched to 0 not finished
Switched to 1 not finished
Switched to 2 not finished
Switched to 3 not finished
Switched to 4 not finished
Switched to 5 not finished
Switched to 6 not finished
Switched to 0 not finished
Switched to 1 finished
Switched to 3 finished
Switched to 5 finished
Switched to 0 finished
Switched to 4 finished
Switched to 2 finished
Switched to 6 finished
Switched to primary";

		if (_log != null) Assert.AreEqual(expected, string.Join(Environment.NewLine, _log));
	}

	private List<IFiber> GenMockFiberWrappers()
	{
		var mockFiberWrappers = new List<IFiber>();
		for (var i = 0; i < 7; ++i)
		{
			var callCnt = 0;
			var actId = i;
			var mockFiberWrapper = new Mock<IFiber>();

			mockFiberWrapper.Setup(x => x.Priority).Returns(actId);
			mockFiberWrapper.Setup(x => x.Id).Returns((uint)actId);
			mockFiberWrapper.Setup(x => x.SwitchToPrimary()).Callback(() => { _log?.Add("Switched to primary"); });
			mockFiberWrapper.Setup(x => x.Switch())
				.Callback(() =>
				{
					_log?.Add(
						callCnt != 2 ? "Switched to " + actId + " not finished" : "Switched to " + actId + " finished"
						); 
					ProcessManager.Switch(callCnt++ == 2);
				});
			mockFiberWrappers.Add(mockFiberWrapper.Object);
		}

		return mockFiberWrappers;
	}

	
}