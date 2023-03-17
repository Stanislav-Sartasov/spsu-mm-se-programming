namespace Fibers.Fibers;

public interface IFiber
{
	public uint Id { get; }
	public int Priority { get; }

	public void Switch();
	public void SwitchToPrimary();
	public void Delete();
}