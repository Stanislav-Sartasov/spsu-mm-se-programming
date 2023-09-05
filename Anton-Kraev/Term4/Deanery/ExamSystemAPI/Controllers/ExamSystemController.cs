using ExamSystem.Implementation;
using ExamSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystemAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExamSystemController : ControllerBase
{
    private static readonly IExamSystem ExamSystem = new StripedExamSystem();

    [HttpGet(Name = "FindStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<bool> FindStudent(long studentId, long courseId)
    {
        bool found;

        try
        {
            found = ExamSystem.Contains(studentId, courseId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        return Ok(found);
    }

    [HttpPost(Name = "AddStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult AddStudent(long studentId, long courseId)
    {
        try
        {
            ExamSystem.Add(studentId, courseId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        return Ok();
    }

    [HttpDelete(Name = "RemoveStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult RemoveStudent(long studentId, long courseId)
    {
        try
        {
            ExamSystem.Remove(studentId, courseId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        return Ok();
    }

    [HttpGet(Name = "GetStudentsCount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<int> GetStudentsCount()
    {
        int count;

        try
        {
            count = ExamSystem.Count;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        return Ok(count);
    }
}