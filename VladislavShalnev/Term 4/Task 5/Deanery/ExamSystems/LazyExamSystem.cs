using Deanery.Collections;

namespace Deanery.ExamSystems;

public class LazyExamSystem : IExamSystem
{
	private readonly LazySet<Tuple<long, long>> _storage;

	public LazyExamSystem() => _storage = new LazySet<Tuple<long, long>>();

	public void Add(long studentId, long courseId) => _storage.Add(new(studentId, courseId));

	public void Remove(long studentId, long courseId) => _storage.Remove(new(studentId, courseId));

	public bool Contains(long studentId, long courseId) => _storage.Сontains(new(studentId, courseId));

	public int Count => _storage.Count;
}