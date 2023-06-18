using Microsoft.AspNetCore.Mvc;

namespace QuizApplication.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
