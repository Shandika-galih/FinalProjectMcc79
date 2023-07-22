using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_leave_histories")]
public class LeaveHistory : BaseEntity
{
    [Column("leave_request_guid")]
    public Guid LeaveRequestGuid { get; set; }

    //cardinality

    public LeaveRequest? LeaveRequest { get; set; }
}
