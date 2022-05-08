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
    public IActionResult Index()
    {
        var loans = (from l in dataBaseContext.Loan
            join dc in dataBaseContext.DvdCopy on l.CopyNumber equals dc.CopyNumber
            join dt in dataBaseContext.DvdTitle on dc.DvdNumber equals dt.DvdNumber
            where l.DateReturned > DateTime.Now orderby l.DateOut
            select new
            {
                dvdTitleNumber = dt.DvdNumber,
                dvdCopyNumber = dc.CopyNumber,
                
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
        var copyNumber = dataBaseContext.DvdCopy.ToArray();
        var membership = dataBaseContext.Member.ToArray();
        ViewBag.copyNumbers = copyNumber;
        ViewBag.memberships = membership;
        ViewBag.loanTypes = loanType;
        return View("~/Views/Forms/AddLoan.cshtml");
    }
}