using ExamSystem.ConcurrentCollections;
using ExamSystem.Interface;

namespace ExamSystem.Implementation;

public class StripedCuckooExamSystem : IExamSystem
{
    private readonly StripedCuckooHashSet<Tuple<long, long>> _store;

    public StripedCuckooExamSystem()
    {
        _store = new StripedCuckooHashSet<Tuple<long, long>>(30);
    }

    public StripedCuckooExamSystem(int capacity)
    {
        _store = new StripedCuckooHashSet<Tuple<long, long>>(capacity);
    }

    public int Count => _store.Count();

    public void Add(long studentId, long courseId) => _store.Add(new(studentId, courseId));

    public bool Contains(long studentId, long courseId) => _store.Contains(new(studentId, courseId));

    public void Remove(long studentId, long courseId) => _store.Remove(new(studentId, courseId));
}