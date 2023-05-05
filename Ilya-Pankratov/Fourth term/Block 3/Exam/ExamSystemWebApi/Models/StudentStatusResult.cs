namespace ExamSystemWebApi;

public class StudentStatusResult : AStudentResult
{
    public string StatusResult { get; set; }
    public static string Added => "Student's exam was added";
    public static string Removed => "Student's exam was removed";

    public static StudentStatusResult GetStudentAdded(long studentId, long courseId)
    {
        return new StudentStatusResult
        {
            StudentId = studentId,
            CourseId = courseId,
            StatusResult = Added
        };
    }

    public static StudentStatusResult GetStudentRemoved(long studentId, long courseId)
    {
        return new StudentStatusResult
        {
            StudentId = studentId,
            CourseId = courseId,
            StatusResult = Removed
        };
    }
}