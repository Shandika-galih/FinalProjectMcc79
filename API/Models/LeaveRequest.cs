using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection.Metadata;

namespace API.Models
{
    [Table("tb_tr_leave_request")]
    public class LeaveRequest : BaseEntity
    {
        [Column("status")]
        public StatusEnum Status { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("submit_date")]
        public DateTime SubmitDate { get; set; }

        [Column("remarks", TypeName = "varchar(max)")]
        public string Remarks { get; set; }

        [Column("attachment")]
        public byte[]? Attachment { get; set; }

        [Column("leave_types_guid")]
        public Guid LeaveTypesGuid { get; set; }

        [Column("employees_guid")]
        public Guid EmployeesGuid { get; set; }


        //cardinality
        public ICollection<LeaveHistory>? LeaveHistories { get; set; }
        public LeaveType? LeaveType { get; set; }
        public Employee? Employee { get; set; }
    }
}
