using QuizApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using QuizApplication.Models.Subject;

namespace QuizApplication.Controllers
{
	[Authorize(Roles = "Admin")]
	public class SubjectController : Controller
	{
		private readonly ISubjectService _subjectService;
		private readonly INotyfService _notyf;

		public SubjectController(ISubjectService subjectService, INotyfService notyfService)
		{
			_subjectService = subjectService;
			_notyf = notyfService;
		}

		public IActionResult Index()
		{
			var response = _subjectService.GetAllSubject();
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

			return View(response.Data);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateSubjectViewModel request)
		{
			var response = _subjectService.CreateSubject(request);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return View(request);
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Subject"); ;
		}

		public IActionResult GetSubject(string id)
		{
			var response = _subjectService.GetSubject(id);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return RedirectToAction("Index", "Subject");
			}

			_notyf.Success(response.Message);

			return View(response.Data);

		}

		public IActionResult Update()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Update(string id, UpdateSubjectViewModel request)
		{
			var response = _subjectService.UpdateSubject(id, request);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return View(request);
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Category");
		}

		[HttpPost]
		public IActionResult DeleteSubject([FromRoute] string id)
		{
			var response = _subjectService.DeleteSubject(id);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return RedirectToAction("Index", "Subject");
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Subject");
		}
	}
}
