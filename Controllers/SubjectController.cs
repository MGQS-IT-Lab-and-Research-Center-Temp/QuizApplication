using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Models.Subject;
using QuizApplication.Service.Interface;

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

            return RedirectToAction("Index", "Question"); ;
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
        public IActionResult UpdateSubject(string id, UpdateSubjectViewModel request)
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

        
    }
}