namespace ExamSystem.Core.Sets.LockFreeHashSet;

internal class AtomicNode<T>
{
	public readonly int Key;
	public readonly T? Value;
	public IAtomicMarkableReference<AtomicNode<T>> CurrentMarkedAndNext;

	public AtomicNode(int key, T value)
	{
		Value = value;
		Key = key;
		CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(null);
	}

	public AtomicNode(int key)
	{
		Value = default;
		Key = key;
		CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(null);
	}
}