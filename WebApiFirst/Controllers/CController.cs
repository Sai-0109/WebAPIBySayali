using Microsoft.AspNetCore.Mvc;

namespace WebApiFirst.Controllers
{
    public class CController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
