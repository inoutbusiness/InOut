using Microsoft.AspNetCore.Mvc;

namespace InOut.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
