using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Models.Question;
using QuizApplication.Service.Interface;

namespace QuizApplication.Controllers
{

    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ISubjectService _subjectService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotyfService _notyf;

        public QuestionController(
            IQuestionService questionService,
            ISubjectService subjectService,
            IHttpContextAccessor httpContextAccessor,
            INotyfService notyf)
        {
            _questionService = questionService;
            _subjectService = subjectService;
            _httpContextAccessor = httpContextAccessor;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestion();
            ViewData["Message"] = questions.Message;
            ViewData["Status"] = questions.Status;

            return View(questions.Data);
        }

		public IActionResult Create()
		{
			ViewBag.Subjects = _subjectService.SelectSubject();
			ViewData["Message"] = "";
			ViewData["Status"] = false;

			return View();
		}

		[HttpPost]
        public IActionResult Create(CreateQuestionViewModel request)
        {
            var response = _questionService.Create(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
        }

        public IActionResult GetQuestionBySubject(string id)
        {
            var response = _questionService.GetQuestionsBySubjectId(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult GetQuestionDetail(string id)
        {
            var response = _questionService.GetQuestion(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _questionService.GetQuestion(id);

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateQuestionViewModel request)
        {
            var response = _questionService.Update(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return RedirectToAction("Index", "Home");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
        }

        [HttpPost]
        public IActionResult DeleteQuestion([FromRoute] string id)
        {
            var response = _questionService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
        }
    }

}
