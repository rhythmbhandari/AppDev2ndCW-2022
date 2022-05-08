using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.ViewModel;
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
        var members = dataBaseContext.Member.ToArray();
        ViewBag.members = members;
        return View();
    }

    public IActionResult NoLoans()
    {
        return View();
    }

    public IActionResult MembershipDetails(int id)
    {
        var member = dataBaseContext.Member.Single(m => m.MemberNumber == id);
        var contextData = (from m in dataBaseContext.Member
            join l in dataBaseContext.Loan on m.MemberNumber equals l.MemberNumber
            join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            where m.MemberNumber == id && l.DateOut > DateTime.Now.AddDays(-31)
            select new
            {
                dvdTitle = dt.DvdNumber,
                dvdCopy = dc.CopyNumber,
                dvdBorrowedDate = l.DateOut,
                dvdReturnDate = l.DateReturned,
                totalLoans = dataBaseContext.Loan.Where(l => l.MemberNumber == id).Count(),
            });
        ViewBag.contextData = contextData;
        ViewBag.firstName = member.MemberFirstName;
        ViewBag.lastName = member.MemberLastName;
        return View("MemberDetails");
    }

    [HttpPost]
    public IActionResult AddMembership(Member member)
    {
        dataBaseContext.Member.Add(member);
        dataBaseContext.SaveChanges();
        return Redirect("/Membership/AddMembership");
    }

    [HttpGet]
    public IActionResult AddMembership()
    {
        var categories = dataBaseContext.MembershipCategory.ToArray();
        ViewBag.membershipCategories = categories;
        return View("~/Views/Forms/AddMembership.cshtml");
    }
    
    
    [Route("addMembershipCategory")]
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddMembershipCategory(MembershipViewModel viewModel)
    {
        if (Request.Method == "POST")
        {
            MembershipCategory category = new MembershipCategory();
            category.MembershipCategoryDescription = viewModel.MembershipCategoryDescription;
            category.MembershipCategoryTotalLoans = viewModel.MembershipCategoryTotalLoans.ToString();
            dataBaseContext.MembershipCategory.Add(category);
            dataBaseContext.SaveChanges();
            return Redirect("/Membership/AddMembership");
        }

        return View("~/Views/Forms/AddMembershipCategory.cshtml");
    }
}