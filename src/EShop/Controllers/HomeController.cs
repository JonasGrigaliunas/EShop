using EShop.Handlers;
using EShop.Models;
using EShop.Readers;
using EShop.Writers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReader<IdentityRole> _roleReader;
        private readonly IReader<ApplicationUser> _userReader;
        private readonly IWriter<IdentityRole> _roleWriter;
        private readonly ITransactionProvider _transactionProvider;

        public HomeController(IReader<IdentityRole> roleReader, IReader<ApplicationUser> userReader, IWriter<IdentityRole> roleWriter, ITransactionProvider transactionProvider)
        {
            _roleReader = roleReader;
            _userReader = userReader;
            _roleWriter = roleWriter;
            _transactionProvider = transactionProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}