using ExamSystem.API.Models;
using ExamSystem.Core.DataStructures;
using ExamSystem.Core.ExamSystems;
using ExamSystem.Core.Sets.LockFreeHashSet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamSystem.API.Controllers;

[ApiController]
[Route("api")]
public class ExamSystemController : ControllerBase
{
	private static readonly IExamSystem ExamSystem;

	static ExamSystemController()
	{
		var set = new LockFreeSet<HashLongTuple>();
		ExamSystem = new Core.ExamSystems.ExamSystem(set);
	}

	[Route("add")]
	public ActionResult Add()
	{
		try
		{
			var examEntry = GetExamEntryFromJson().Result;

			ExamSystem.Add(examEntry.Student, examEntry.Course);
		}
		catch
		{
			return BadRequest();
		}

		return Ok();
	}

	[Route("remove")]
	public ActionResult Remove()
	{
		try
		{
			var examEntry = GetExamEntryFromJson().Result;

			ExamSystem.Remove(examEntry.Student, examEntry.Course);
		}
		catch
		{
			return BadRequest();
		}

		return Ok();
	}

	[Route("contains")]
	public ActionResult Contains()
	{
		try
		{
			var examEntry = GetExamEntryFromJson().Result;

			var result = ExamSystem.Contains(examEntry.Student, examEntry.Course);

			return Content(result.ToString());
		}
		catch
		{
			return BadRequest();
		}
	}

	[Route("count")]
	public ActionResult Count()
	{
		return Content(ExamSystem.Count.ToString());
	}


	private async Task<ExamEntryModel> GetExamEntryFromJson()
	{
		var req = Request.Body;
		var json = await new StreamReader(req).ReadToEndAsync();
		return JsonConvert.DeserializeObject<ExamEntryModel>(json);
	}
}