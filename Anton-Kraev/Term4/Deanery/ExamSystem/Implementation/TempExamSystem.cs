using ExamSystem.Interface;

namespace ExamSystem.Implementation;

public class TempExamSystem : IExamSystem
{
    public int Count => 0;

    public void Add(long studentId, long courseId)
    {
    }

    public bool Contains(long studentId, long courseId)
    {
        return false;
    }

    public void Remove(long studentId, long courseId)
    {
    }
}