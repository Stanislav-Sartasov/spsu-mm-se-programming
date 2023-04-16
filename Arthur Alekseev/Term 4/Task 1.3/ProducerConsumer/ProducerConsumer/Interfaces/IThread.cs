namespace ProducerConsumer.Interfaces;

public interface IThread
{
	public void Start();
	public void Stop();
	public void Wait();
}