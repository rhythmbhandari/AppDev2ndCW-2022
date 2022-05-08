using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev2ndCW_2022.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long UserNumber { get; set; }
 
        public string? name { get; set; }
        public string? UserName { get; set; }


        public string? contacts { get; set; }

        public string? UserType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Type must be between 5 and 100 characters long")]

        public string? UserPassword { get; set; }
    }
}
