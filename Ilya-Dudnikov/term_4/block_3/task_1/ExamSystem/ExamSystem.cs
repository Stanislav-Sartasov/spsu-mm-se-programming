namespace ExamSystem;

public class ExamSystem : IExamSystem
{
    private ISet<Credit> set;

    public ExamSystem(ISet<Credit> set)
    {
        this.set = set;
    }
    
    public void Add(long studentId, long courseId) => set.Add(new Credit(studentId, courseId));

    public void Remove(long studentId, long courseId) => set.Remove(new Credit(studentId, courseId));

    public bool Contains(long studentId, long courseId) => set.Contains(new Credit(studentId, courseId));

    public int Count => set.Count;
}