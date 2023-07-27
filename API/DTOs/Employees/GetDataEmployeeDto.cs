using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class GetDataEmployeeDto
    {
        public Guid Guid { get; set; }
        public int NIK { get; set; }
        public string FullName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int EligibleLeave { get; set; }
        public DateTime HiringDate { get; set; }
        public Guid? ManagerGuid { get; set; }
        public string Manager { get; set; }
        public string RoleName { get; set; }
    }
}
