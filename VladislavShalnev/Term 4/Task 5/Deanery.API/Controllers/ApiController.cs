using Deanery.ExamSystems;
using Microsoft.AspNetCore.Mvc;

namespace Deanery.API.Controllers;

[ApiController]
[Route("api/[action]")]
public class ApiController : ControllerBase
{
	private static LazyExamSystem _examSystem = new();

	private readonly ILogger<ApiController> _logger;

	public ApiController(ILogger<ApiController> logger)
	{
		_logger = logger;
	}

	[HttpGet(Name = "count")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
	public ActionResult<int> Count() => _examSystem.Count;

	[HttpGet(Name = "add")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult Add(long studentId, long courseId)
	{
		_examSystem.Add(studentId, courseId);

		return Ok();
	}

	[HttpGet(Name = "remove")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult Remove(long studentId, long courseId)
	{
		_examSystem.Remove(studentId, courseId);

		return Ok();
	}

	[HttpGet(Name = "contains")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
	public ActionResult<bool> Contains(long studentId, long courseId)
	{
		bool contains = _examSystem.Contains(studentId, courseId);

		return Ok(contains);
	}
}