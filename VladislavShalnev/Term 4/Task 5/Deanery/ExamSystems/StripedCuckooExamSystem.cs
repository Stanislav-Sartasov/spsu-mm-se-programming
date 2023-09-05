using Deanery.Collections;

namespace Deanery.ExamSystems;

public class StripedCuckooExamSystem : IExamSystem
{
	private readonly StripedCuckooHashSet<Tuple<long, long>> _storage;
	private readonly object _locker = new();

	public StripedCuckooExamSystem() => _storage = new StripedCuckooHashSet<Tuple<long, long>>(32);

	public void Add(long studentId, long courseId)
	{
		if (!_storage.Add(new(studentId, courseId))) return;

		lock (_locker)
		{
			Count++;
		}
	}

	public void Remove(long studentId, long courseId)
	{
		if (!_storage.Remove(new(studentId, courseId))) return;

		lock (_locker)
		{
			Count--;
		}
	}

	public bool Contains(long studentId, long courseId) => _storage.Contains(new(studentId, courseId));

	public int Count { get; private set; }
}