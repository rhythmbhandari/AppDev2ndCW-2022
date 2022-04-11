using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class MembershipCategory
    {
        [Key]
        public long MembershipCategoryNumber { get; set; }
        public string MembershipCategoryDescription { get; set; }
        public string MembershipCategoryTotalLoans { get; set; }
    }
}
