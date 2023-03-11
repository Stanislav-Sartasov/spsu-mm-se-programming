using Fibers.Fibers;
using Fibers.ProcessManager;

namespace FiberTests;

public class FiberWrapperTest
{
	[Test]
	public void FiberWrapperCreate()
	{
		Assert.DoesNotThrow(() =>
		{
			Process sampleProcess = new();

			FiberWrapper f = new(sampleProcess);

			Assert.IsNotNull(f);

			Assert.NotNull(f.Id);

			Assert.AreEqual(f.Priority, sampleProcess.Priority);
		});
	}



	[Test]
	public void FiberWrapperSwitchToPrimary()
	{
		Process sampleProcess = new();

		FiberWrapper f = new(sampleProcess);

		Assert.DoesNotThrow(() => { f.SwitchToPrimary(); });
	}

	[Test]
	public void FiberWrapperDelete()
	{
		Process sampleProcess = new();

		FiberWrapper f = new(sampleProcess);

		Assert.DoesNotThrow(() => { f.Delete(); });
	}
}