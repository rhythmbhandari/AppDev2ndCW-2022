using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

    
    //Question number 8
    [Authorize]
    public IActionResult AllMembers()
    {
        var members = dataBaseContext.Member.ToArray();
        List<AllMembersViewModel> membersList = new List<AllMembersViewModel>();
        foreach (var member in members)
        {
            AllMembersViewModel allMembersViewModel = new AllMembersViewModel();
            var loans = dataBaseContext.Loan.Where(x => x.MemberNumber == member.MemberNumber && x.DateReturned == null).Count();
            var totalAllowedLoans = (from m in dataBaseContext.Member
                join mc in dataBaseContext.MembershipCategory on m.MembershipCategoryNumber equals mc
                    .MembershipCategoryNumber
                where m.MemberNumber == member.MemberNumber
                select new
                {
                    allowedLoan = mc.MembershipCategoryTotalLoans,
                }).ToArray()[0];
            allMembersViewModel.member = member;
            allMembersViewModel.count = loans;
            allMembersViewModel.totalAllowedLoans = long.Parse(totalAllowedLoans.allowedLoan);
            membersList.Add(allMembersViewModel);

        }
        ViewBag.members = membersList;
        return View();
    }
    
    //Question number 3
    [Authorize]
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
    [Authorize]
    public IActionResult AddMembership(Member member)
    {
        dataBaseContext.Member.Add(member);
        dataBaseContext.SaveChanges();
        return Redirect("/Membership/AddMembership");
    }

    [HttpGet]
    [Authorize]
    public IActionResult AddMembership()
    {
        var categories = dataBaseContext.MembershipCategory.ToArray();
        ViewBag.membershipCategories = categories;
        return View("~/Views/Forms/AddMembership.cshtml");
    }
    
    
    //Question number 12
    [Authorize]
    public IActionResult MembersWithNoLoans()
    {
        List<NotLoanedViewModel> notLoanedViewModels = new List<NotLoanedViewModel>();
        var members = (from m in dataBaseContext.Member
            join l in dataBaseContext.Loan on m.MemberNumber equals l.MemberNumber
            where l.DateOut < DateTime.Now.AddDays(-31)
            select new
            {
                memberId = m.MemberNumber,
                memberName = m.MemberFirstName + " " + m.MemberLastName,
                memberAddress = m.MemberAddress,
            }).Distinct().ToArray();
        foreach (var member in members)
        {
            NotLoanedViewModel notLoanedViewModel = new NotLoanedViewModel();
            notLoanedViewModel.memberName = member.memberName;
            notLoanedViewModel.memberId = member.memberId;
            notLoanedViewModel.memberAddress = member.memberAddress;

            var lastLoan = (from l in dataBaseContext.Loan
                join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
                join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
                where l.MemberNumber == member.memberId
                orderby l.DateOut descending
                select new
                {
                    dvdName = dt.DvdName,
                    daysSinceLastLoan = (DateTime.Now - l.DateOut).Days,
                    dateOut = l.DateOut
                }).ToArray()[0];
            notLoanedViewModel.dvdTitle = lastLoan.dvdName;
            notLoanedViewModel.dateOut = lastLoan.dateOut;
            notLoanedViewModel.daysSinceLastLoan = lastLoan.daysSinceLastLoan;
            notLoanedViewModels.Add(notLoanedViewModel);
        }

        notLoanedViewModels.Sort((x, y) => DateTime.Compare(x.dateOut, y.dateOut));
        var distinct = notLoanedViewModels.Distinct().ToList();
        ViewBag.context = distinct;
        ViewBag.name = "ram";
        ViewBag.members = members;
        return View();
    }
    

    [Route("addMembershipCategory")]
    [AcceptVerbs("Get", "Post")]
    [Authorize]
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