namespace ExamSystem.Core.Sets.LockFreeHashSet;

internal class MarkedReference<T>
{
	public readonly bool Marked;
	public readonly T Value;

	public MarkedReference(T value, bool marked)
	{
		Value = value;
		Marked = marked;
	}
}