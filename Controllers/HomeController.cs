using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TpfinalBack.Models;
using TpfinalBack.Filters;

namespace TpfinalBack.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AcercaDe()
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
