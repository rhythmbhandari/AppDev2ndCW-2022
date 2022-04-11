using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class DvdCategory
    {
        [Key]
        public long CategoryNumber { get; set; }

        [Required]
        public string CategoryDescription { get; set; }

        [Required]
        public string AgeRestricted { get; set; }

    }
}
