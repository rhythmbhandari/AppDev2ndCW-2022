using AppDev2ndCW_2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppDev2ndCW_2022.Controllers;

public class MembershipController: Controller
{ 
    public readonly DataBaseContext dataBaseContext;
    private readonly ILogger<MembershipController> _logger;

    public MembershipController(ILogger<MembershipController> logger, DataBaseContext db)
    {
        dataBaseContext = db;
        _logger = logger;
    }

    public IActionResult AllMembers()
    {
        return View();
    }

    public IActionResult NoLoans()
    {
        return View();
    }
    
}