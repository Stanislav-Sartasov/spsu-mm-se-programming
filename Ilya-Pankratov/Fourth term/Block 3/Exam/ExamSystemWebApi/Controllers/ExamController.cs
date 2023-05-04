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
    public ActionResult<StudentResult> Contains(long studentId, long courseId)
    {
        StudentResult studentResult;
        if (examSystem.Contains(studentId, courseId))
        {
            studentResult = StudentResult.GetStudentPassed(studentId, courseId);
        }
        else
        {
            studentResult = StudentResult.GetStudentFailed(studentId, courseId);
        }

        return Ok(studentResult);
    }

    [HttpPost("add", Name = "Add")]
    public ActionResult<string> Add(long studentId, long courseId)
    {
        examSystem.Add(studentId, courseId);
        return Ok("Added");
    }

    [HttpDelete("remove", Name = "Remove")]
    public ActionResult<string> Remove(long studentId, long courseId)
    {
        examSystem.Add(studentId, courseId);
        return Ok("Removed");
    }

    [HttpGet("count", Name = "Count")]
    public ActionResult<int> Count()
    {
        return Ok(examSystem.Count);
    }
}