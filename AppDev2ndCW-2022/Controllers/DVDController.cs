using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    
    // Question number 4
    [Authorize]
    public IActionResult AllDvds()
    {
        List<DvdListViewModel> dvdList = new List<DvdListViewModel>();
        var allDvds = dataBaseContext.DvdTitle.OrderBy(x => x.DateReleased).ToArray();
        foreach (var dvd in allDvds)
        {
            DvdListViewModel dvdListViewModel = new DvdListViewModel();
            dvdListViewModel.dvdName = dvd.DvdName;
            dvdListViewModel.actors = new List<string>();
            var actors = (from c in dataBaseContext.CastMember
                join a in dataBaseContext.Actor on c.ActorNumber equals a.ActorNumber
                where c.DvdNumber == dvd.DvdNumber orderby a.ActorSurname
                select new
                {
                    actorName = a.ActorFirstName + " " + a.ActorSurname,
                }).ToArray();
            foreach (var actor in actors)
            {
                dvdListViewModel.actors.Add(actor.actorName);
            }

            var producer = dataBaseContext.Producer.Single(x => x.ProducerNumber == dvd.ProducerNumber);
            var studio = dataBaseContext.Studio.Single(x => x.StudioNumber == dvd.StudioNumber);

            dvdListViewModel.producerName = producer.ProducerName;
            dvdListViewModel.studioName = studio.StudioName;
            dvdList.Add(dvdListViewModel);
        }

        ViewBag.context = dvdList;
        return View();
    }

    //Question number 11
    [Authorize]
    public IActionResult Loaned(DateTime? date)
    {
        if(date is not null)
        {
            var loansByDate = (from l in dataBaseContext.Loan
                join m in dataBaseContext.Member on l.MemberNumber equals m.MemberNumber
                join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
                join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
                where l.DateReturned == null && l.DateOut == date
                orderby l.DateOut
                select new
                {
                    dvdTitle = dt.DvdName,
                    memberName = m.MemberFirstName + " " + m.MemberLastName,
                    copyNumber = dc.CopyNumber,
                    totalPerDay = dataBaseContext.Loan.Count(x => x.DateOut.Date == l.DateOut && x.DateReturned == null),
                    dateOut = l.DateOut.ToString("dd/MM/yyyy"),
                });
            ViewBag.loans = loansByDate;
            return View();
        }
        else if (date is null)
        {
            var loansByDate = (from l in dataBaseContext.Loan
                join m in dataBaseContext.Member on l.MemberNumber equals m.MemberNumber
                join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
                join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
                where l.DateReturned == null
                orderby l.DateOut
                select new
                {
                    dvdTitle = dt.DvdName,
                    memberName = m.MemberFirstName + " " + m.MemberLastName,
                    copyNumber = dc.CopyNumber,
                    totalPerDay = dataBaseContext.Loan.Count(x => x.DateOut.Date == l.DateOut && x.DateReturned == null),
                    dateOut = l.DateOut.ToString("dd/MM/yyyy"),
                });
            ViewBag.loans = loansByDate;
            return View();
        }

        return View();
    }

 
    //Question number 13
    [Authorize]
    public IActionResult NotLoaned()
    {
        var context = (from l in dataBaseContext.Loan
            join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            where l.DateOut < DateTime.Now.AddDays(-31)
            select new 
            {
                dvdTitle = dt.DvdName,
                lastDateOut = l.DateOut.ToString("dd/MM/yyyy"),
            }).Distinct().ToArray();
        ViewBag.dvds = context;
        return View();
    }



    /*Controller for actor add form*/
    [Route("addActor")]
    [AcceptVerbs("Get", "Post")]
    [Authorize]
    public IActionResult AddActor(Actor actor)
    {
        if (Request.Method == "POST")
        {
            dataBaseContext.Add(actor);
            dataBaseContext.SaveChanges();
            return Redirect("/DVD/AddDvd");
        }
        return View("~/Views/Forms/AddActor.cshtml");
    }

    //Question number 9
    [HttpPost]
    [Authorize]
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
    [Authorize]
    public IActionResult AddDvd()
    {
        ViewBag.actors = dataBaseContext.Actor.ToArray();
        ViewBag.categories = dataBaseContext.DvdCategory.ToArray();
        ViewBag.studios = dataBaseContext.Studio.ToArray();
        ViewBag.producers = dataBaseContext.Producer.ToArray();
        return View("~/Views/Forms/AddDVD.cshtml");
    }
    
    /*Controller for removing DVDs older than a year*/
    //Question number 10
    [Authorize]
    public IActionResult RemoveDvds()
    {
        var dvds = (from dc in dataBaseContext.DvdCopy
            join l in dataBaseContext.Loan on dc.CopyNumber equals l.CopyNumber
            where l.DateReturned != null && dc.DatePurchased.AddDays(365) < DateTime.Now
            select dc).ToArray();
        
        dataBaseContext.Set<DvdCopy>().RemoveRange(dvds);
        dataBaseContext.SaveChanges();
        return View("RemovalSuccessful");
    }
    
    
    [Authorize]
    public IActionResult RemoveDvdConfirmation()
    {
        var dvds = (from dc in dataBaseContext.DvdCopy
            join l in dataBaseContext.Loan on dc.CopyNumber equals l.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            where l.DateReturned != null && dc.DatePurchased.AddDays(365) < DateTime.Now
            select new
            {
                dvdCopy = dc.CopyNumber,
                dvdTitle = dt.DvdName,
            });
        ViewBag.check = DateTime.Now.AddDays(365);
        ViewBag.dvds = dvds;
        return View("RemovalConfirmation");
    }
    
    
    /*Controller for adding studio*/

    [Route("addStudio")]
    [AcceptVerbs("Get", "Post")]
    [Authorize]
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
    
    /*Controller for adding producers*/
    [Authorize]
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
    
    /*Controller for adding categories*/
    [Authorize]
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
    
    /*Controller for adding DVD copies*/
    [Authorize]
    [HttpPost]
    public IActionResult AddDvdCopy(DvdCopy dvdCopy)
    {
        dataBaseContext.DvdCopy.Add(dvdCopy);
        dataBaseContext.SaveChanges();
        return Redirect("/DVD/AddDvdCopy");
    }

    [HttpGet]
    [Authorize]
    public IActionResult AddDvdCopy()
    {
        var dvdTitle = dataBaseContext.DvdTitle.ToArray();
        ViewBag.dvdTitles = dvdTitle;
        return View("~/Views/Forms/AddDVDCopy.cshtml");
    }
}