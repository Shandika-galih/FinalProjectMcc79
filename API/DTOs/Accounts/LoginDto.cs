using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

