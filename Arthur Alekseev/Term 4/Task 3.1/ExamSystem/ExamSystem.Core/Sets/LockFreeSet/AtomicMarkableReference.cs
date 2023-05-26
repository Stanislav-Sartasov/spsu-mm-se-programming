namespace ExamSystem.Core.Sets.LockFreeHashSet;

internal class AtomicMarkableReference<T> : IAtomicMarkableReference<T>
{
	private volatile MarkedReference<T> _markedRef;

	public AtomicMarkableReference(T initialValue)
	{
		_markedRef = new MarkedReference<T>(initialValue, false);
	}

	public AtomicMarkableReference(T initialValue, bool initialMark)
	{
		_markedRef = new MarkedReference<T>(initialValue, initialMark);
	}

	public bool CompareAndSet(T expectedReference, T newReference, bool expectedMark, bool newMark)
	{
		if (_markedRef.Value is null)
			return false;

		var currentPair = _markedRef;

		if (!currentPair.Value.Equals(expectedReference) || currentPair.Marked != expectedMark)
			return false;

		var newPair = new MarkedReference<T>(newReference, newMark);

		return Interlocked.CompareExchange(ref _markedRef, newPair, currentPair) == currentPair;
	}

	public T Get(out bool marked)
	{
		marked = _markedRef.Marked;
		return _markedRef.Value;
	}

	public T GetReference()
	{
		return _markedRef.Value;
	}

	public void Set(T newReference, bool newMark)
	{
		if (newReference is null)
			throw new ArgumentNullException(nameof(newReference));

		if (newReference.Equals(_markedRef.Value) && newMark == _markedRef.Marked)
			throw new ArgumentException();

		var newRef = new MarkedReference<T>(newReference, newMark);

		Interlocked.Exchange(ref _markedRef, newRef);
	}
}