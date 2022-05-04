using Microsoft.AspNetCore.Mvc;

namespace AppDev2ndCW_2022.Controllers;

public class LoanController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}