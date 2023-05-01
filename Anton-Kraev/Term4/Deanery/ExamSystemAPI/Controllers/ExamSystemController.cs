using ExamSystem;
using ExamSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystemAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExamSystemController : ControllerBase
{

    private readonly ILogger<ExamSystemController> _logger;
    private readonly IExamSystem _examSystem;

    public ExamSystemController(ILogger<ExamSystemController> logger)
    {
        _logger = logger;
        _examSystem = new TempExamSystem();
    }

    [HttpGet(Name = "FindStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<bool> FindStudent(long studentId, long courseId)
    {
        bool found;

        try
        {
            found = _examSystem.Contains(studentId, courseId);
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
            _examSystem.Add(studentId, courseId);
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
            _examSystem.Remove(studentId, courseId);
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
            count = _examSystem.Count;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        return Ok(count);
    }
}