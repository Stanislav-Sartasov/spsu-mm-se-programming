namespace Deanery.Collections;

public class MonitorLock
{
	private object _locker = new object();

	public void Lock() => Monitor.Enter(_locker);

	public void Unlock() => Monitor.Exit(_locker);
}