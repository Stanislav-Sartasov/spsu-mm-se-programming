namespace ExamSystem.Core.Sets.LockFreeHashSet;

public class LockFreeSet<T> : Interface.ISet<T>
{
	private readonly AtomicNode<T> _head;

	public LockFreeSet()
	{
		AtomicNode<T> tail = new(int.MaxValue);

		_head = new AtomicNode<T>(int.MinValue)
		{
			CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(tail)
		};
	}

	public bool Contains(T item)
	{
		if (item is null)
			throw new ArgumentNullException(nameof(item));

		var key = item.GetHashCode();
		var current = _head;
		var marked = false;

		while (current.Key < key)
		{
			current = current.CurrentMarkedAndNext.GetReference();
			current.CurrentMarkedAndNext.Get(out marked);
		}

		return current.Key == key && !marked;
	}

	public bool Add(T item)
	{
		if (item is null)
			throw new ArgumentNullException(nameof(item));

		var key = item.GetHashCode();

		while (true)
		{
			var (previous, current) = Find(_head, key);

			if (current.Key == key)
				return false;

			var node = new AtomicNode<T>(key, item)
			{
				CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(current, false)
			};

			if (previous.CurrentMarkedAndNext.CompareAndSet(current, node, false, false))
				return true;
		}
	}

	public bool Remove(T item)
	{
		if (item is null)
			throw new ArgumentNullException(nameof(item));

		var key = item.GetHashCode();

		while (true)
		{
			var (previous, current) = Find(_head, key);

			if (current.Key != key)
				return false;

			var successor = current.CurrentMarkedAndNext.GetReference();

			var snip = current.CurrentMarkedAndNext.CompareAndSet(successor, successor, false, true);

			if (!snip)
				continue;

			previous.CurrentMarkedAndNext.CompareAndSet(current, successor, false, false);

			return true;
		}
	}

	private static (AtomicNode<T>, AtomicNode<T>) Find(AtomicNode<T> head, int key)
	{
		while (true)
		{
			var previous = head;
			var current = previous.CurrentMarkedAndNext.GetReference();

			while (true)
			{
				var proceedWithNextCycle = false;

				var successor = current.CurrentMarkedAndNext.Get(out var marked);

				while (marked)
				{
					var snip = previous.CurrentMarkedAndNext.CompareAndSet(current, successor, false, false);

					if (!snip)
					{
						proceedWithNextCycle = true;
						break;
					}

					current = successor;
					successor = current.CurrentMarkedAndNext.Get(out marked);
				}

				if (proceedWithNextCycle)
					break;

				if (current.Key >= key)
					return (previous, current);

				previous = current;
				current = successor;
			}
		}
	}
}