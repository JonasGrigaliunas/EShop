using Microsoft.AspNetCore.Mvc;

namespace EShop.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
