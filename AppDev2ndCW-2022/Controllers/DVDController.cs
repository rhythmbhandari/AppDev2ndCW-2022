using Microsoft.AspNetCore.Mvc;

namespace AppDev2ndCW_2022.Controllers;

public class DVDController : Controller
{
    // GET
    public IActionResult AllMembers()
    {
        return View();
    }

    public IActionResult Loaned()
    {
        return View();
    }

    public IActionResult NotLoaned()
    {
        return View();
    }
}