using CreditSystem;
using Microsoft.AspNetCore.Mvc;

namespace CreditSystem;

[Route("api")]
public class CreditController : Controller
{
    private readonly ICreditSystem creditSystem;

    public CreditController(ICreditSystem creditSystem)
    {
        this.creditSystem = creditSystem;
    }

    [HttpPost("add/{courseId}/{studentId}")]
    public ActionResult Add(long courseId, long studentId)
    {
        creditSystem.Add(courseId, studentId);
        return Ok();
    }
    
    [HttpDelete("remove/{courseId}/{studentId}")]
    public ActionResult Remove(long courseId, long studentId)
    {
        creditSystem.Remove(courseId, studentId);
        return Ok();
    }
    
    [HttpGet("contains/{courseId}/{studentId}")]
    public ActionResult Contains(long courseId, long studentId)
    {
        return Content(creditSystem.Contains(courseId, studentId).ToString());
    }
    
    [HttpGet("count")]
    public ActionResult Count()
    {
        return Content(creditSystem.Count().ToString());
    }
}