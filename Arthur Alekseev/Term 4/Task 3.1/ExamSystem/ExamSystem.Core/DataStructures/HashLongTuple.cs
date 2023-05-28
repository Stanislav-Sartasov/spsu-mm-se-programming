namespace ExamSystem.Core.DataStructures;

public class HashLongTuple
{
	private const int FirstOffset = 0x541234;
	public readonly (long, long) Value;

	public HashLongTuple(long itemOne, long itemTwo)
	{
		Value = (itemOne, itemTwo);
	}

	public override int GetHashCode()
	{
		return (int)(Value.Item1 * FirstOffset + Value.Item2);
	}

	public override bool Equals(object? obj)
	{
		if (obj is not HashLongTuple objAsTuple)
			return false;

		return objAsTuple.Value.Item1 == Value.Item1
		       && objAsTuple.Value.Item2 == Value.Item2;
	}
}