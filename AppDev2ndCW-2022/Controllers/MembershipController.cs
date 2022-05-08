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
        return View();
    }

    public IActionResult NoLoans()
    {
        return View();
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