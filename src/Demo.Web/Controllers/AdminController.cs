using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
