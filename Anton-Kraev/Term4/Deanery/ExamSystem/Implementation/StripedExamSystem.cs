using ExamSystem.ConcurrentCollections;
using ExamSystem.Interface;

namespace ExamSystem.Implementation;

public class StripedExamSystem : IExamSystem
{
    private readonly StripedHashSet<Tuple<long, long>> _store;

    public StripedExamSystem()
    {
        _store = new StripedHashSet<Tuple<long, long>>(32);
    }

    public StripedExamSystem(int capacity)
    {
        _store = new StripedHashSet<Tuple<long, long>>(capacity);
    }

    public int Count => _store.Count();

    public void Add(long studentId, long courseId) => _store.Add(new (studentId, courseId));

    public bool Contains(long studentId, long courseId) => _store.Contains(new (studentId, courseId));

    public void Remove(long studentId, long courseId) => _store.Remove(new (studentId, courseId));
}