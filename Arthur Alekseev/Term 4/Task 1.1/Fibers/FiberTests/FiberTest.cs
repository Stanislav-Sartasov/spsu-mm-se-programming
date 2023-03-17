using Fibers.Fibers;
using Fibers.ProcessManager;

namespace FiberTests;

public class FiberTest
{
	[Test]
	public void FiberCreate()
	{
		Assert.DoesNotThrow(() =>
		{
			Process sampleProcess = new();

			Fiber f = new(sampleProcess.Run);

			Assert.IsNotNull(f);

			Assert.NotNull(f.Id);

			Assert.NotNull(f.IsPrimary);
		});
	}


	[Test]
	public void FiberDelete()
	{
		Process sampleProcess = new();

		Fiber f = new(sampleProcess.Run);

		Assert.DoesNotThrow(() => { f.Delete(); });
	}
}