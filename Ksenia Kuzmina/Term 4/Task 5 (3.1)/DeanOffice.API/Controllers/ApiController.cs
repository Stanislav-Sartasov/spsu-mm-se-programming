using DeanOffice.ExamSystems;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeanOffice.API.Controllers
{
	[Route("Api")]
	public class ApiController : ControllerBase
	{
		private static IExamSystem examSystem;

		static ApiController()
		{
			examSystem = new LazyExamSystem();
		}

		[Route("Add")]
		public ActionResult Add()
		{
			var examEntry = GetData().Result;

			examSystem.Add(examEntry.Student, examEntry.Course);

			return Ok();
		}

		[Route("Remove")]
		public ActionResult Remove()
		{
			var examEntry = GetData().Result;

			examSystem.Remove(examEntry.Student, examEntry.Course);

			return Ok();
		}

		[Route("Contains")]
		public ActionResult Contains()
		{
			var examEntry = GetData().Result;

			var result = examSystem.Contains(examEntry.Student, examEntry.Course);

			return Content(result.ToString());
		}

		[Route("Count")]
		public ActionResult Count()
		{
			return Content(examSystem.Count.ToString());
		}

		private async Task<StudentCoursePair> GetData()
		{
			var req = Request.Body;
			var json = await new StreamReader(req).ReadToEndAsync();
			return JsonConvert.DeserializeObject<StudentCoursePair>(json);
		}
	}
}