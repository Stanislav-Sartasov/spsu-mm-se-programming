namespace ExamLib;

public class StudentPassedEqualityComparator : IEqualityComparer<StudentPassedExam>
{
    public bool Equals(StudentPassedExam? x, StudentPassedExam? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.StudentId == y.StudentId && x.CourseId == y.CourseId;
    }

    public int GetHashCode(StudentPassedExam obj)
    {
        return (int)obj.CourseId;
    }
}