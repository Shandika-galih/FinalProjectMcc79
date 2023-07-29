using API.Utilities.Enums;
using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Employee
{
    public class AddEmployeeVM
    {
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "0 = Female, 1 = Male ")]
        public GenderEnum Gender { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public int EligibleLeave { get; set; }

        [Required]
        public DateTime HiringDate { get; set; }

        public Guid? ManagerGuid { get; set; }

        [Required]
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
