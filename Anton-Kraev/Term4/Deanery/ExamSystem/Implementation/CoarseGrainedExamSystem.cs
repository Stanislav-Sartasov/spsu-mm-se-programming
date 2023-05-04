using ExamSystem.ConcurrentCollections;
using ExamSystem.Interface;

namespace ExamSystem.Implementation;

public class CoarseGrainedExamSystem : IExamSystem
{
    private readonly CoarseGrainedHashSet<(long, long)> _store;

    public CoarseGrainedExamSystem()
    {
        _store = new CoarseGrainedHashSet<(long, long)>(30);
    }

    public CoarseGrainedExamSystem(int capacity)
    {
        _store = new CoarseGrainedHashSet<(long, long)>(capacity);
    }

    public int Count => _store.Count();

    public void Add(long studentId, long courseId) => _store.Add((studentId, courseId));

    public bool Contains(long studentId, long courseId) => _store.Contains((studentId, courseId));

    public void Remove(long studentId, long courseId) => _store.Remove((studentId, courseId));
}