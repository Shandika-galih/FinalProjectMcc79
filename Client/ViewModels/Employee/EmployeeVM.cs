using API.Utilities.Enums;

namespace Client.ViewModels.Employee;

public class EmployeeVM
{
    public Guid Guid { get; set; }
    public int NIK { get; set; }
    public string? FullName { get; set; } 
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public GenderEnum Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int EligibleLeave { get; set; }
    public DateTime HiringDate { get; set; }
    public Guid? ManagerGuid { get; set; }
    public string Manager { get; set; }
    public string RoleName { get; set; }
	public Guid RoleGuid { get; set; }
	public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
