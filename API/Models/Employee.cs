using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_employees")]
    public class Employee : BaseEntity
    {
        [Column("nik")]
        public int NIK { get; set; }

        [Column("first_name", TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [Column("gender")]
        public GenderEnum Gender { get; set; }

        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        [Column("eligible_leave")]
        public int EligibleLeave { get; set; }

        [Column("manager_guid")]
        public Guid? ManagerGuid { get; set; }

        //Cardinality 
        public Employee? Manager { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public Account? Account { get; set; }    
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }
}
