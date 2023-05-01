namespace ExamSystem.Interface;

public interface IExamSystem
{
    public void Add(long studentId, long courseId);
    public void Remove(long studentId, long courseId);
    public bool Contains(long studentId, long courseId);
    public int Count { get; }
}