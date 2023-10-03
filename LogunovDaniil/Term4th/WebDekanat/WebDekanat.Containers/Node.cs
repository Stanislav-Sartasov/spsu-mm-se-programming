namespace WebDekanat.Containers
{
	internal class Node<T>
	{
		private Mutex mutex = new();

		public T Value;
		public long Key;

		public volatile bool Marked = false;
		public Node<T> Next;

		public Node(long key)
		{
			Key = key;
		}

		public Node(T value)
		{
			Value = value;
			Key = value.GetHashCode();
		}

		public void Lock()
		{
			mutex.WaitOne();
		}

		public void Unlock()
		{
			mutex.ReleaseMutex();
		}

		public override int GetHashCode()
		{
			return (int)Key;
		}
	}
}
