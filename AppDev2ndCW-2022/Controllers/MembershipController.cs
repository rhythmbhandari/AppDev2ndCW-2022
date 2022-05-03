using AppDev2ndCW.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppDev2ndCW_2022.Controllers;

public class MembershipController: Controller
{ 
    public readonly DataBaseContext dataBaseContext;
    private readonly ILogger<HomeController> _logger;

    public MembershipController()
    {
        
    }
}