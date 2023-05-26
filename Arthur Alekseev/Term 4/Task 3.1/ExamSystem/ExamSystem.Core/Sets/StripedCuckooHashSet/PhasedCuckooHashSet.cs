namespace ExamSystem.Core.Sets.PhasedCuckooHashSet;

public abstract class PhasedCuckooHashSet<T> : Interface.ISet<T>
{
	// list is semi-full
	private const int Threshold = 100;

	// list is full
	protected const int ListSize = 128;

	// steps to relocate
	private const int Limit = 40;

	private const int HashStride = 0x2345;

	protected volatile int Capacity;
	protected volatile List<T>[,] Table;

	protected PhasedCuckooHashSet(int size)
	{
		Capacity = size;
		Table = new List<T>[2, Capacity];

		for (var i = 0; i < 2; i++)
		for (var j = 0; j < Capacity; j++)
			Table[i, j] = new List<T>(ListSize);
	}

	public bool Contains(T x)
	{
		Acquire(x);
		try
		{
			var set0 = Table[0, Hash0(x) % Capacity];

			if (set0.Contains(x))
				return true;

			var set1 = Table[1, Hash1(x) % Capacity];
			return set1.Contains(x);
		}
		finally
		{
			Release(x);
		}
	}

	public bool Remove(T x)
	{
		Acquire(x);
		try
		{
			var set0 = Table[0, Hash0(x) % Capacity];
			if (set0.Contains(x))
			{
				set0.Remove(x);
				return true;
			}

			var set1 = Table[1, Hash1(x) % Capacity];

			if (!set1.Contains(x))
				return false;

			set1.Remove(x);

			return true;
		}
		finally
		{
			Release(x);
		}
	}

	public bool Add(T x)
	{
		Acquire(x);
		var h0 = Hash0(x) % Capacity;
		var h1 = Hash1(x) % Capacity;

		var i = -1;
		var h = -1;

		var mustResize = false;

		try
		{
			if (Contains(x))
				return false;

			var set0 = Table[0, h0];
			var set1 = Table[1, h1];

			if (set0.Count < Threshold)
			{
				set0.Add(x);
				return true;
			}
			else if (set1.Count < Threshold)
			{
				set1.Add(x);
				return true;
			}
			else if (set0.Count < ListSize)
			{
				set0.Add(x);
				i = 0;
				h = h0;
			}
			else if (set1.Count < ListSize)
			{
				set1.Add(x);
				i = 1;
				h = h1;
			}
			else
			{
				mustResize = true;
			}
		}
		finally
		{
			Release(x);
		}

		if (mustResize)
		{
			Resize();
			Add(x);
		}
		else if (!Relocate(i, h))
		{
			Resize();
		}

		return true;
	}

	protected int Hash0(T item)
	{
		if (item is null)
			throw new ArgumentNullException(nameof(item));

		return Math.Abs(item.GetHashCode());
	}

	protected int Hash1(T item)
	{
		if (item is null)
			throw new ArgumentNullException(nameof(item));

		return Math.Abs(item.GetHashCode() + HashStride);
	}

	protected abstract void Acquire(T x);
	protected abstract void Release(T x);
	protected abstract void Resize();

	private bool Relocate(int i, int hi)
	{
		var hj = 0;
		var j = 1 - i;
		for (var round = 0; round < Limit; round++)
		{
			var iSet = Table[i, hi];
			var y = iSet[0];

			hj = i switch
			{
				0 => Hash1(y) % Capacity,
				1 => Hash0(y) % Capacity,
				_ => hj
			};

			Acquire(y);

			var jSet = Table[j, hj];

			try
			{
				if (iSet.Remove(y))
					switch (jSet.Count)
					{
						case < Threshold:
							jSet.Add(y);
							return true;

						case < ListSize:
							jSet.Add(y);
							i = 1 - i;
							hi = hj;
							j = 1 - j;
							break;

						default:
							iSet.Add(y);
							return false;
					}
				else if (iSet.Count < Threshold) return true;
			}
			finally
			{
				Release(y);
			}
		}

		return false;
	}
}