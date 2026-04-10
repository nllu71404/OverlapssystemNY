using Microsoft.AspNetCore.Mvc;

namespace OverlapssystemAPI.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
