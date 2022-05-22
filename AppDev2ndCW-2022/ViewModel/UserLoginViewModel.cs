using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.ViewModel;

public class UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}