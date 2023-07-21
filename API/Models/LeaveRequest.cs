using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tr_m_leave_request")]
    public class LeaveRequest
    {
        [Column("status")]
        public Enum Status { get; set; }
       
        [Column("start_date")]
        public DateOnly Startdate  { get; set; }

        [Column("end_date")]
        public DateOnly Enddate { get; set; }

        [Column("submit_date")]
        public DateTime SubmitDate { get; set; }

        [Column("remarks", TypeName = "varchar(max)")]
        public string Remarks { get; set; }


        [Column("eligible_leave")]
        public int EligibleLeave {get; set; }

        [Column("total_leave")]
        public int TotalLeave { get; set; }

        [Column("leave_types")]
        public Guid LeaveTypes { get; set; }

        [Column("Employees")]
        public Guid Employees { get; set; }
    }
}
