using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Account;

public class ForgotPasswordVM
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
