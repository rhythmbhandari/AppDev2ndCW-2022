using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppDev2ndCW_2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly DataBaseContext dataBaseContext;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, DataBaseContext db, UserService userService)
        {
            _logger = logger;
            dataBaseContext = db;
            _userService = userService;
        }

        public IActionResult IndexTest(bool Islogout = false)
        {
            ViewBag.islogout = Islogout;
            return View();
        }

        public IActionResult Register(User users)
        {
            users.UserName = "admin";
            users.contacts = "1234567890";
            users.name = "admin";
            users.UserPassword = "admin";
            users.UserType = "admin";
            dataBaseContext.User.Add(users);
            dataBaseContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string contact, string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;

            if (_userService.TryValidateUser(email, contact, out List<Claim> claims))
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
                    return RedirectToAction("Dashboard", "Users", new { IsLogin = true });
                }
            }
            else
            {
                TempData["Error"] = "Invalid username or password";
                return Redirect("/");
            }
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
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