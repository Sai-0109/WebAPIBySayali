using Microsoft.AspNetCore.Mvc;

namespace WebApiFirst.Controllers
{
    public class BController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
