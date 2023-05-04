namespace ProducerConsumer;

public abstract class Member
{
	private readonly Thread _thread;
	private readonly Semaphore _sem;
	protected List<int> list;
	private readonly CancellationTokenSource _tokenSrc;

	protected Member(Semaphore sem, List<int> list, string name, CancellationTokenSource tokenSrc)
	{
		_sem = sem;
		this.list = list;
		_thread = new Thread(ThreadAction);
		_thread.Name = name;
		_tokenSrc = tokenSrc;
	}

	protected abstract void Act();

	private void ThreadAction()
	{
		while (!_tokenSrc.Token.IsCancellationRequested)
		{
			_sem.WaitOne();
			Act();
			_sem.Release();
			Thread.Sleep(1000);
		}
	}

	public void Start() => _thread.Start();

	public void Stop()
	{
		if (!_tokenSrc.Token.IsCancellationRequested)
			_tokenSrc.Cancel();

		_thread.Join();
	}
}