using AppDev2ndCW_2022.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppDev2ndCW_2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly DataBaseContext dataBaseContext;

        public HomeController(ILogger<HomeController> logger, DataBaseContext db)
        {
            _logger = logger;
            dataBaseContext = db;
        }

        public IActionResult Index(String actor, String radio)
        {
            if (actor == null)
            {
                return View();
            }

            var actors = dataBaseContext.Actor.Where(x => x.ActorFirstName.Contains(actor));
            ViewBag.allActors = actors;
            return View("SearchResult");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SearchResult()
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