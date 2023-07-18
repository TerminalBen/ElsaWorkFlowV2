using Microsoft.AspNetCore.Mvc;


namespace ElsaDemo.Controllers
{
    public class ElsaDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}