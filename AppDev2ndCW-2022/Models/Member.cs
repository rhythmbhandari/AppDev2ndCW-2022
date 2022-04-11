using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class Member
    {
        [Key]
        public long MemberNumber { get; set; }
        public long MembershipCategoryNumber { get; set; }
        public string MemberLastName { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberAddress { get; set; }
        public DateTime MemberDOB { get; set; }

    }
}
