namespace ExamSystemWebApi;

public class StudentExamResult : AStudentResult
{
    public string ExamResult { get; set; }

    public static string Passed => "Passed";
    public static string Failed => "Failed";

    public static StudentExamResult GetStudentPassed(long studentId, long courseId)
    {
        return new StudentExamResult
        {
            StudentId = studentId,
            CourseId = courseId,
            ExamResult = Passed
        };
    }

    public static StudentExamResult GetStudentFailed(long studentId, long courseId)
    {
        return new StudentExamResult
        {
            StudentId = studentId,
            CourseId = courseId,
            ExamResult = Failed
        };
    }
}