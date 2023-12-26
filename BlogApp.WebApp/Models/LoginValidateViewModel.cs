using System.ComponentModel.DataAnnotations;

namespace BlogApp.WebApp.Models;

public class LoginValidateViewModel
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
