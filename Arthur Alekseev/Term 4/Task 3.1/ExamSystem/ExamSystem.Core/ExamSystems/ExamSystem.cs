using ExamSystem.Core.DataStructures;

namespace ExamSystem.Core.ExamSystems;

public class ExamSystem : IExamSystem
{
	private readonly Sets.Interface.ISet<HashLongTuple> _set;

	public ExamSystem(Sets.Interface.ISet<HashLongTuple> set)
	{
		_set = set;
	}

	public int Count { get; private set; }

	public void Add(long studentId, long courseId)
	{
		var success = _set.Add(new HashLongTuple(studentId, courseId));

		if (!success)
			throw new ArgumentException("Element already added");

		lock (this)
		{
			Count++;
		}
	}

	public void Remove(long studentId, long courseId)
	{
		var success = _set.Remove(new HashLongTuple(studentId, courseId));

		if (!success)
			throw new ArgumentException("Element already removed");

		lock (this)
		{
			Count--;
		}
	}

	public bool Contains(long studentId, long courseId)
	{
		return _set.Contains(new HashLongTuple(studentId, courseId));
	}
}