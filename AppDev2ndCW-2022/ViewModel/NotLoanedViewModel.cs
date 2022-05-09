namespace AppDev2ndCW_2022.ViewModel;

public class NotLoanedViewModel
{
    public long memberId { get; set; }
    public string memberName { get; set; }
    public string memberAddress { get; set; }
    public DateTime dateOut { get; set; }
    public  string dvdTitle { get; set; }
    public int daysSinceLastLoan { get; set; }
}