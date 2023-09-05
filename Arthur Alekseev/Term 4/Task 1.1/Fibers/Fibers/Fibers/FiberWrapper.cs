using Fibers.ProcessManager;

namespace Fibers.Fibers;

public class FiberWrapper : IFiber
{
	// Needs to be a field or else it will be removed by GC
	private readonly Fiber _fiber;

	public FiberWrapper(Process process)
	{
		Priority = process.Priority;
		_fiber = new Fiber(process.Run);
		Id = _fiber.Id;
	}

	public uint Id { get; }
	public int Priority { get; }

	public void Switch()
	{
		Fiber.Switch(Id);
	}

	public void Delete()
	{
		Fiber.Delete(Id);
	}

	public void SwitchToPrimary()
	{
		// Only for demo purposes
		Console.WriteLine("Switched to primary");
		Fiber.Switch(Fiber.PrimaryId);
	}
}