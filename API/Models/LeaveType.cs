using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tr_m_leave_types")]
    public class LeaveType : BaseEntity
    {
        [Column("leave_name", TypeName = "nvarchar(50)")]
        public string LeaveName { get; set; }

        [Column("leave_day")]
        public int LeaveDay { get; set; }

        [Column("leave_description", TypeName = "varchar(max)")]
        public string LeaveDescription { get; set;}

        //cardinality
        public LeaveRequest? LeaveRequest { get; set;}
    }
}
