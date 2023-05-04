namespace ExamSystemWebApi;

public class StudentResult
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public string ExamResult { get; set; }

    public static string Passed => "Passed";
    public static string Failed => "Failed";

    public static StudentResult GetStudentPassed(long studentId, long courseId)
    {
        return new StudentResult 
            { 
                StudentId = studentId, 
                CourseId = courseId, 
                ExamResult = Passed 
            };
    }

    public static StudentResult GetStudentFailed(long studentId, long courseId)
    {
        return new StudentResult
        {
            StudentId = studentId,
            CourseId = courseId,
            ExamResult = Failed
        };
    }
}