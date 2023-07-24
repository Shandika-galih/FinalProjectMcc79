using API.Utilities;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employees
{
    public class AddEmployeeDto
    {
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "0 = Female, 1 = Male ")]
        public GenderEnum Gender { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public int EligibleLeave { get; set; }

        public Guid? ManagerGuid { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPolicy]
        public string Password { get; set; }

        [Required]
        [ConfirmPassword("NewPassword", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
