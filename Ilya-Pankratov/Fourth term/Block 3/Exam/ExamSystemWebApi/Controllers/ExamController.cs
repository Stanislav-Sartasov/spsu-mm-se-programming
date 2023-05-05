using System.Runtime.Serialization;
using ExamLib;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystemWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ExamController : ControllerBase
{
    private IExamSystem examSystem;

    public ExamController(IExamSystem examSystem)
    {
        this.examSystem = examSystem;
    }

    [HttpGet("contains", Name = "Contains")]
    public ActionResult<StudentExamResult> Contains(long studentId, long courseId)
    {
        StudentExamResult studentExamResult;
        if (examSystem.Contains(studentId, courseId))
        {
            studentExamResult = StudentExamResult.GetStudentPassed(studentId, courseId);
        }
        else
        {
            studentExamResult = StudentExamResult.GetStudentFailed(studentId, courseId);
        }

        return Ok(studentExamResult);
    }

    [HttpPost("add", Name = "Add")]
    public ActionResult<StudentStatusResult> Add(long studentId, long courseId)
    {
        examSystem.Add(studentId, courseId);
        return Ok(StudentStatusResult.GetStudentAdded(studentId, courseId));
    }

    [HttpDelete("remove", Name = "Remove")]
    public ActionResult<StudentStatusResult> Remove(long studentId, long courseId)
    {
        examSystem.Remove(studentId, courseId);
        return Ok(StudentStatusResult.GetStudentRemoved(studentId, courseId));
    }

    [HttpGet("count", Name = "Count")]
    public ActionResult<int> Count()
    {
        return Ok(examSystem.Count);
    }
}