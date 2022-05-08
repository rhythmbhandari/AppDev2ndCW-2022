namespace AppDev2ndCW_2022.ViewModel;

public class DvdViewModel
{
    public long StudioNum { get; set; }
    public string DvdName { get; set; }
    public long ProducerNum { get; set; }
    public long CategoryNum { get; set; }
    public int StandardCharge { get; set; }
    public int PenaltyCharge { get; set; }
    
    public DateTime DatePosted { get; set; }
}