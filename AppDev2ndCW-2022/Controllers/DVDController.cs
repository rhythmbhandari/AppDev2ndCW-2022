using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.ViewModel;
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
        var dvd = (from d in dataBaseContext.DvdTitle
            join c in dataBaseContext.CastMember on d.DvdNumber equals c.DvdNumber
            join a in dataBaseContext.Actor on c.ActorNumber equals a.ActorNumber
            join s in dataBaseContext.Studio on d.StudioNumber equals s.StudioNumber
            join p in dataBaseContext.Producer on d.ProducerNumber equals p.ProducerNumber
            orderby d.DateReleased
            select new
            {
                title = d.DvdNumber,
                studio = s.StudioName,
                producer = p.ProducerName,
            });
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

    [HttpPost]
    public IActionResult AddDvd(DvdViewModel viewModel, List<long> castMembers)
    {
        var timeNow = TimeOnly.FromDateTime(DateTime.Now);
        DvdTitle dvdTitle = new DvdTitle();
        dvdTitle.ProducerNumber = viewModel.ProducerNum;
        dvdTitle.DvdName = viewModel.DvdName;
        dvdTitle.CategoryNumber = viewModel.CategoryNum;
        dvdTitle.PenaltyCharge = viewModel.PenaltyCharge;
        dvdTitle.StudioNumber = viewModel.StudioNum;
        dvdTitle.DateReleased = viewModel.DatePosted;
        dvdTitle.StandardCharge = viewModel.StandardCharge;
        dataBaseContext.DvdTitle.Add(dvdTitle);
        dataBaseContext.SaveChanges();

        foreach (var actor in castMembers)
        {
            CastMember castMember = new CastMember();
            castMember.ActorNumber = actor;
            castMember.DvdNumber = dvdTitle.DvdNumber;
            dataBaseContext.CastMember.Add(castMember);
            dataBaseContext.SaveChanges();
        }

        return Redirect("/DVD/AddDvd");
    }
    
    /*Controller for add DVD get*/
    [HttpGet]
    public IActionResult AddDvd()
    {
        ViewBag.actors = dataBaseContext.Actor.ToArray();
        ViewBag.categories = dataBaseContext.DvdCategory.ToArray();
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
            dataBaseContext.Studio.Add(studio);
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
    
    /*Controller for adding DVD copy*/
    [HttpPost]
    public IActionResult AddDvdCopy(DvdCopy dvdCopy)
    {
        dataBaseContext.DvdCopy.Add(dvdCopy);
        dataBaseContext.SaveChanges();
        return Redirect("/DVD/AddDvdCopy");
    }

    [HttpGet]
    public IActionResult AddDvdCopy()
    {
        var dvdTitle = dataBaseContext.DvdTitle.ToArray();
        ViewBag.dvdTitles = dvdTitle;
        return View("~/Views/Forms/AddDVDCopy.cshtml");
    }
}