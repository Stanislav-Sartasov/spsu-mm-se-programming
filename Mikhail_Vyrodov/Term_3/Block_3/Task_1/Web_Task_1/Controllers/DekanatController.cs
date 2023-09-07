using Microsoft.AspNetCore.Mvc;
using Task_1.Collections;

namespace Web_Task_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DekanatController : ControllerBase
    {
        private static IExamSystem examSystem = new StripedCuckooHashSet(200);

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(examSystem.Count);
        }

        [HttpGet("Contains")]
        public IActionResult GetExamInfo(long studentId, long courseId)
        {
            return Ok(examSystem.Contains(studentId, courseId));
        }

        [HttpPost("Add")]
        public IActionResult Add(long studentId, long courseId)
        {
            examSystem.Add(studentId, courseId);

            return Ok("added");
        }

        [HttpPost("Remove")]
        public IActionResult Remove(long studentId, long courseId)
        {
            examSystem.Remove(studentId, courseId);

            return Ok("removed");
        }
    }
}