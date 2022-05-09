using AppDev2ndCW_2022.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using AppDev2ndCW_2022.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AppDev2ndCW_2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly DataBaseContext dataBaseContext;
        public readonly UserService _UserService;

        public HomeController(ILogger<HomeController> logger, DataBaseContext db, UserService userService)
        {
            _UserService = userService;
            _logger = logger;
            dataBaseContext = db;
        }
        

        //Homepage - Search bar | Question no 1 and Question no 2
        public IActionResult Index(String actor, String radio, bool IsLogout = false)
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
                        DateReleased = t.DateReleased.ToString("dd/MM/yyyy"),
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
                        DateReleased = t.DateReleased.ToString("dd/MM/yyyy"),
                        StandardCharge = t.StandardCharge,
                        PenaltyCharge = t.PenaltyCharge,
                    }).ToArray();
                ViewBag.allDvd = dvd;
            }
            return View("SearchResult");
        }

        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            /*if (Login == admin)
            {
                return RedirectToAction("Home", "Admin");
            }
            else
            {
                return RedirectToAction("Home", "Users");
            }*/
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string email, string contact, string ReturnUrl)
        {
            //login functionality
            ViewData["ReturnUrl"] = ReturnUrl;

            if (_UserService.TryValidateUser(email,contact, out List<Claim> claims))
            {
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Privacy", "Home", new { IsLogin = true });
                }
            }
            else
            {
                TempData["Error"] = "Invalid username or password";
                return Redirect("/");
            }
        }


        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/login");
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