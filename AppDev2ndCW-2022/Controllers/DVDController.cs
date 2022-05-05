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

    /*Controller for actor add form*/
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
    public IActionResult AddDvd()
    {
        ViewBag.actors = dataBaseContext.Actor.ToArray();
        ViewBag.studios = dataBaseContext.Studio.ToArray();
        ViewBag.producers = dataBaseContext.Producer.ToArray();
        return View("~/Views/Forms/AddDVD.cshtml");
    }
    
    /*Controller for studio add form*/
    [Route("addStudio")]
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddStudio(Studio studio)
    {
        if (Request.Method == "POST")
        {
            dataBaseContext.Add(studio);
            dataBaseContext.SaveChanges();
            return Redirect("/DVD/AddDvd");
        }
        return View("~/Views/Forms/AddStudio.cshtml");
    }
    
    /*Controller for producer add form*/
    [Route("addProducer")]
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddProducer(Producer producer)
    {
        if (Request.Method == "POST")
        {
            dataBaseContext.Add(producer);
            dataBaseContext.SaveChanges();
            return Redirect("/DVD/AddDvd");
        }
        return View("~/Views/Forms/AddProducer.cshtml");
    }
    
    /*Controller for category add form*/
    [Route("addCategory")]
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddCategory(DvdCategory category)
    {
        if (Request.Method == "POST")
        {
            dataBaseContext.Add(category);
            dataBaseContext.SaveChanges();
            return Redirect("/DVD/AddDvd");
        }
        return View("~/Views/Forms/AddCategory.cshtml");
    }
}