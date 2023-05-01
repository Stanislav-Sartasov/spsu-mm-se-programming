namespace DeansOffice;

public class ExamSystemS : IExamSystem
{
    private LockFreeSet<(long, long)> lockFreeSet;

    public ExamSystemS()
    {
        lockFreeSet = new LockFreeSet<(long, long)>();
    }

    public int Count => lockFreeSet.Count();

    public void Add(long studentId, long courseId)
    {
        lockFreeSet.Add((studentId, courseId));
    }

    public void Remove(long studentId, long courseId)
    {
        lockFreeSet.Remove((studentId, courseId));
    }

    public bool Contains(long studentId, long courseId)
    {
        return lockFreeSet.Contains((studentId, courseId));
    }
}