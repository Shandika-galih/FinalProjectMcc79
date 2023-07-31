using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Account;

public class ChangePasswordVM
{
    public string Email { get; set; }
    public int Otp { get; set; }
    [Required]
    [PasswordPolicy]
    public string NewPassword { get; set; }
    [Required]
    [ConfirmPassword("NewPassword", ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
