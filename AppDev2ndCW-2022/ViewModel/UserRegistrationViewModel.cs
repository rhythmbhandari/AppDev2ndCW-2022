using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.ViewModel;

public class UserRegistrationViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
    public string ConfirmPassword { get; set; }
}