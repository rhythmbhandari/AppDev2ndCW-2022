using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class LoanTypes
    {
        [Key]
        public long LoanTypeNumber { get; set; }
        public string? LoanType { get; set; }
        public int LoanDuration { get; set; }

    }
}
