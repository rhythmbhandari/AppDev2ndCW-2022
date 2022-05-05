using AppDev2ndCW_2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppDev2ndCW_2022.Controllers;

public class DVDController : Controller
{
    private readonly ILogger<DVDController> _logger;
    public readonly DataBaseContext dataBaseContext;

    public DVDController(ILogger<DVDController> logger, DataBaseContext db)
    {
        _logger = logger;
        dataBaseContext = db;
    }
    // GET
    public IActionResult AllMembers()
    {
        ViewBag.message = "Success";
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

    [Route("addActor")]
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddActor(Actor actor)
    {
        if (Request.Method == "POST")
        {
            dataBaseContext.Add(actor);
            dataBaseContext.SaveChanges();
            return Redirect("/DVD/AllMembers");
        }
        return View("~/Views/Forms/AddActor.cshtml");
    }
    
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddDvd(DvdTitle DVD)
    {
        ViewBag.actors = dataBaseContext.Actor.ToArray();
        return View("~/Views/Forms/AddDVD.cshtml");
    }
}