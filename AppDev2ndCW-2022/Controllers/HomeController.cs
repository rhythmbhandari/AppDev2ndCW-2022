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

            if (radio == "all")
            {
                var dvd = (from t in dataBaseContext.DvdTitle
                    join c in dataBaseContext.CastMember on t.DvdNumber equals c.DvdNumber
                    join a in dataBaseContext.Actor on c.ActorNumber equals a.ActorNumber
                    where a.ActorSurname == actor
                    select new
                    {
                        DvdName = t.DvdName,
                        DvdNumber = c.DvdNumber,
                        DateReleased = t.DateReleased,
                        StandardCharge = t.StandardCharge,
                        PenaltyCharge = t.PenaltyCharge,
                    }).ToArray();
                ViewBag.allDvd = dvd;
            }
            else if (radio == "available")
            {
                var dvd = (from t in dataBaseContext.DvdTitle
                    join c in dataBaseContext.CastMember on t.DvdNumber equals c.DvdNumber
                    join a in dataBaseContext.Actor on c.ActorNumber equals a.ActorNumber
                    join dc in dataBaseContext.DvdCopy on t.DvdNumber equals dc.DvdNumber
                    join l in dataBaseContext.Loan on dc.CopyNumber equals l.CopyNumber
                    where a.ActorSurname == actor && l.DateReturned == null
                    select new
                    {
                        DvdName = t.DvdName,
                        DvdNumber = c.DvdNumber,
                        DateReleased = t.DateReleased,
                        StandardCharge = t.StandardCharge,
                        PenaltyCharge = t.PenaltyCharge,
                    }).ToArray();
                ViewBag.allDvd = dvd;
            }
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