using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AppDev2ndCW_2022.Models
{
    public class User: IdentityUser
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
