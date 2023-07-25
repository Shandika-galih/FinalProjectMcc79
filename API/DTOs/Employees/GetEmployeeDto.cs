using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class GetEmployeeDto
    {
        public Guid Guid { get; set; }
        public int NIK { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int EligibleLeave { get; set; }
        public DateTime HiringDate { get; set; }
        public Guid? ManagerGuid { get; set; }
    }
}
