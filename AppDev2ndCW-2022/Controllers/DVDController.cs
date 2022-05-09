using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.ViewModel;
using Coursework.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult Loaned()
    {
        var loansByDate = dataBaseContext.Loan.Count();
        return View();
    }

    public IActionResult NotLoaned()
    {
        var context = (from l in dataBaseContext.Loan
            join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            where l.DateOut < DateTime.Now.AddDays(-31)
            select new 
            {
                dvdTitle = dt.DvdName,
                lastDateOut = l.DateOut
            }).Distinct().ToArray();
        ViewBag.dvds = context;
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
            return Redirect("/DVD/AddDvd");
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
    
    /*Controller for removing DVDs older than a year*/
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

    //Question 11
    // Function for DVD copies on loan
    // It shows Copies of DVD on loan
    //[Authorize(Roles = "Manager, Assistant")]
    //[Authorize]
    [HttpGet]
    public IActionResult DVDCopiesOnLoan()
    {
        List<DVDCopiesLoanDTO> dvdCopiesLoanDtos = new List<DVDCopiesLoanDTO>(); //DVDCopiesLoanDTO object
        List<DvdTitle> dvdTitles = dataBaseContext.DvdTitle.ToList(); // converting data of dvd title to list
        List<DvdCopy> dvdCopies = new List<DvdCopy>(); // DVD copy object
        List<Loan> loans = new List<Loan>(); // Loan object
        Member member = new Member(); // Member object
        foreach (var dvdTitle in dvdTitles) // foreach loop to show dvd copies loan
        {
            dvdCopies = dataBaseContext.DvdCopy.Include(x => x.DvdTitle).Where(x => x.DvdTitle == dvdTitle).ToList();
            foreach (var dvdCopy in dvdCopies)
            {
                loans = dataBaseContext.Loan.Include(x => x.DvdCopy).Include(x => x.Member).Where(x => x.DvdCopy == dvdCopy && x.status == "Loaned").ToList();
                if (loans != null)
                {
                    foreach (var loan in loans)
                    {
                        member = dataBaseContext.Member.Where(x => x.MemberNumber == loan.Member.MemberNumber).First();
                        DVDCopiesLoanDTO copiesLoanDto = new DVDCopiesLoanDTO();
                        copiesLoanDto.dateOut = loan.DateOut;
                        copiesLoanDto.title = dvdTitle.DvdName;
                        copiesLoanDto.name = member.MemberFirstName + "" + member.MemberLastName;
                        copiesLoanDto.copyNumber = dvdCopy.CopyNumber;
                        dvdCopiesLoanDtos.Add(copiesLoanDto);
                    }
                }
            }
        }

        dvdCopiesLoanDtos.OrderBy(x => x.dateOut).ThenBy(x => x.title);
        return View(dvdCopiesLoanDtos);
    }
}