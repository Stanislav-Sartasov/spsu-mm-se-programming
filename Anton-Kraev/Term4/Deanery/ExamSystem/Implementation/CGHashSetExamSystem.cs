using ExamSystem.ConcurrentCollections;
using ExamSystem.Interface;

namespace ExamSystem.Implementation;

public class CGHashSetExamSystem : IExamSystem
{
    private readonly CoarseGrainedHashSet<(long, long)> _store = new(100);

    public int Count => _store.Count();

    public void Add(long studentId, long courseId) => _store.Add((studentId, courseId));

    public bool Contains(long studentId, long courseId) => _store.Contains((studentId, courseId));

    public void Remove(long studentId, long courseId) => _store.Remove((studentId, courseId));
}