using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class User
    {
        [Key]
        public long UserNumber { get; set; }
        public string? UserType { get; set; }
        public string? UserPassword { get; set; }

    }
}
