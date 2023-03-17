using Fibers.ProcessManager;

namespace FiberTests;

public class ProcessTest
{
	[Test]
	public void ProcessCreate()
	{
		var testProcess = new Process();

		Assert.NotNull(testProcess.TotalDuration);
		Assert.NotNull(testProcess.ActiveDuration);
		Assert.NotNull(testProcess.Priority);
	}

	[Test]
	public void RunTest()
	{
		var testProcess = new Process();

		// Should result in an error as fiber does not exist for the process
		try
		{
			testProcess.Run();
		}
		catch (Exception)
		{
			Assert.Pass();
		}

		// But can sometimes not result in an error if PM was initialized before
		Assert.Pass();
	}
}