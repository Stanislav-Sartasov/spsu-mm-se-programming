using ExamSystem.ConcurrentCollections;
using ExamSystem.Interface;

namespace ExamSystem.Implementation;

public class CoarseGrainedExamSystem : IExamSystem
{
    private readonly CoarseGrainedHashSet<Tuple<long, long>> _store;

    public CoarseGrainedExamSystem()
    {
        _store = new CoarseGrainedHashSet<Tuple<long, long>>(30);
    }

    public CoarseGrainedExamSystem(int capacity)
    {
        _store = new CoarseGrainedHashSet<Tuple<long, long>>(capacity);
    }

    public int Count => _store.Count();

    public void Add(long studentId, long courseId) => _store.Add(new (studentId, courseId));

    public bool Contains(long studentId, long courseId) => _store.Contains(new (studentId, courseId));

    public void Remove(long studentId, long courseId) => _store.Remove(new (studentId, courseId));
}