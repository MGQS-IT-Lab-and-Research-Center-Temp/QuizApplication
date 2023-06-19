using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Models.Answer;
using QuizApplication.Service.Interface;

namespace QuizApplication.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;
        private readonly INotyfService _notyf;

        public AnswerController(IAnswerService answerService, INotyfService notyf)
        {
            _answerService = answerService;
            _notyf = notyf;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAnswerViewModel request)
        {
            var response = _answerService.WriteAnswer(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateAnswerViewModel request)
        {
            var response = _answerService.ChangeAnswer(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
        }
    }
}