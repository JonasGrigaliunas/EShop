using Microsoft.AspNetCore.Mvc;

namespace EShop.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
