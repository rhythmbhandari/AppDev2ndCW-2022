using AppDev2ndCW_2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppDev2ndCW_2022.Controllers;

public class LoanController : Controller
{
    public readonly DataBaseContext dataBaseContext;

    public LoanController(DataBaseContext db)
    {
        dataBaseContext = db;
    }
    // GET
    public IActionResult Index(int? copyNumber)
    {
        if (copyNumber is not null)
        {
            var loan = (from l in dataBaseContext.Loan
                join m in dataBaseContext.Member on l.MemberNumber equals m.MemberNumber
                join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
                join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
                where l.CopyNumber == copyNumber orderby l.DateOut
                select new
                {
                    memberName = m.MemberFirstName + " " + m.MemberLastName,
                    dateOut = l.DateOut.Date,
                    dateDue = l.DateDue.Date,
                    dateReturned = l.DateReturned,
                    dvdName = dt.DvdName
                }).First();
            ViewBag.loans = loan;
            return View("CopyLoanInfo");
        }
        var loans = (from l in dataBaseContext.Loan
            join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            join m in dataBaseContext.Member on l.MemberNumber equals m.MemberNumber
            where l.DateReturned == null
            select new
            {
                loanId = l.LoanNumber,
                memberName = m.MemberFirstName + " " + m.MemberLastName,
                dvdName = dt.DvdName,
                dvdTitleNumber = dt.DvdNumber,
                dvdCopyNumber = dc.CopyNumber,
                dateIssued = l.DateOut.Date,
            });
        ViewBag.loans = loans;
        return View();
    }

    [Route("addLoanType")]
    [AcceptVerbs("Get", "Post")]
    public IActionResult AddLoanType(LoanTypes loanTypes)
    {
        if (Request.Method == "POST")
        {
            dataBaseContext.LoanTypes.Add(loanTypes);
            dataBaseContext.SaveChanges();
            return Redirect("/Loan/AddLoan");
        }

        return View("~/Views/Forms/AddLoanType.cshtml");

    }
    
    
    [HttpPost]
    public IActionResult AddLoan(Loan loan)
    {
        dataBaseContext.Loan.Add(loan);
        dataBaseContext.SaveChanges();

        return Redirect("/Loan/AddLoan");
    }
    
    [HttpGet]
    public IActionResult AddLoan()
    {
        var loanType = dataBaseContext.LoanTypes.ToArray();
        /*var copyNumber = dataBaseContext.DvdCopy.ToArray();*/
        var copyNumber = (from c in dataBaseContext.DvdCopy
            join t in dataBaseContext.DvdTitle on c.DvdNumber equals t.DvdNumber
            select new
            {
                DvdLoanTitle = "Copy number: " + c.DvdNumber + " | Title: " + t.DvdName,
                CopyNumber = c.CopyNumber,
            });
        var membership = dataBaseContext.Member.ToArray();
        ViewBag.copyNumbers = copyNumber;
        ViewBag.memberships = membership;
        ViewBag.loanTypes = loanType;
        return View("~/Views/Forms/AddLoan.cshtml");
    }

    public IActionResult ReturnLoan(int id)
    {
        var loan = dataBaseContext.Loan.Single(x => x.LoanNumber == id);
        loan.DateReturned = DateTime.Now;
        var noOfDays = (DateTime.Now - loan.DateDue).Days;
        var penaltyCharge = (from l in dataBaseContext.Loan
            join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            where dt.DvdNumber == dc.DvdNumber && dc.CopyNumber == l.CopyNumber
            select new
            {
                penaltyCharge = dt.PenaltyCharge
            }).First();
        var totalPenalty = loan.DateReturned > loan.DateDue ? noOfDays * penaltyCharge.penaltyCharge : 0;
        ViewBag.totalPenalty = totalPenalty;
        dataBaseContext.SaveChanges();
        return View();
    }
}