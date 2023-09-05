namespace ExamLib;

public class StudentPassedExam
{
    public long StudentId { get; }
    public long CourseId { get; }

    public StudentPassedExam(long studentId, long courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }

    public override int GetHashCode()
    {
        return (int)(StudentId + CourseId * 1000000);
    }
}