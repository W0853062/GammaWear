using GammaWear.Data;
using GammaWear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GammaWear.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GammaWearContext _context;

        public HomeController(ILogger<HomeController> logger, GammaWearContext context)
        {  
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var socks = _context.Socks.ToList();
            return View(socks);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
